using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using doremi.Data;
using doremi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace doremi.Services
{
    public class Functional : IFunctional
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRoles _roles;
        private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;

        public Functional(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           IRoles roles,
           IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _roles = roles;
            _superAdminDefaultOptions = superAdminDefaultOptions.Value;
        }

        public async Task InitAppData()
        {
            try
            {
                await _context.BillType.AddAsync(new BillType { BillTypeName = "Default" });
                await _context.SaveChangesAsync();

                Currency currency = new Currency { CurrencyName = "USD", CurrencyCode = "USD" };
                await _context.Currency.AddAsync(currency);
                await _context.SaveChangesAsync();

                await _context.Branch.AddAsync(new Branch { BranchName = "Master", CurrencyId = currency.CurrencyId });
                await _context.SaveChangesAsync();

                await _context.Warehouse.AddAsync(new Warehouse { WarehouseName = "Master" });
                await _context.SaveChangesAsync();

                await _context.CashBank.AddAsync(new CashBank { CashBankName = "Default" });
                await _context.SaveChangesAsync();

                await _context.InvoiceType.AddAsync(new InvoiceType { InvoiceTypeName = "Default" });
                await _context.SaveChangesAsync();

                await _context.PaymentType.AddAsync(new PaymentType { PaymentTypeName = "Default" });
                await _context.SaveChangesAsync();

                await _context.PurchaseType.AddAsync(new PurchaseType { PurchaseTypeName = "Default" });
                await _context.SaveChangesAsync();

                await _context.SalesType.AddAsync(new SalesType { SalesTypeName = "", Description = "", From = new DateTime(1900, 1, 1), To = new DateTime(2200, 12, 31), DiscountPercentage = 0.0 });
                await _context.SalesType.AddAsync(new SalesType { SalesTypeName = "Normal", Description = "Normal", From = new DateTime(1900, 1, 1), To = new DateTime(2200, 12, 31), DiscountPercentage = 0.0 });
                await _context.SalesType.AddAsync(new SalesType { SalesTypeName = "Chistmas 20%", Description = "Chistmas", From = new DateTime(2019, 12, 20), To = new DateTime(2019, 12, 26), DiscountPercentage = 0.2 });
                await _context.SalesType.AddAsync(new SalesType { SalesTypeName = "Mar 10%", Description = "Mar", From = new DateTime(2019, 3, 1), To = new DateTime(2019, 3, 20), DiscountPercentage = 0.1 });
                await _context.SaveChangesAsync();

                await _context.ShipmentType.AddAsync(new ShipmentType { ShipmentTypeName = "Default" });
                await _context.SaveChangesAsync();

                await _context.UnitOfMeasure.AddAsync(new UnitOfMeasure { UnitOfMeasureName = "PCS" });
                await _context.SaveChangesAsync();

                List<ProductType> ProductTypes = new List<ProductType>() {
                    new ProductType { ProductTypeName = "Music Book",Description = "Music Books Product" },
                    new ProductType { ProductTypeName = "Music Score",Description = "Music Scores Product" }
                };
                await _context.ProductType.AddRangeAsync(ProductTypes);
                await _context.SaveChangesAsync();

                List<Group> groups = new List<Group>()
                {
                    new Group { GroupName = "Warehouse", GroupDescription = "Warehouse Group" },
                    new Group { GroupName = "Sales", GroupDescription = "Sales Group" },
                    new Group { GroupName = "Operation", GroupDescription = "Operation Group" },
                    new Group { GroupName = "Account", GroupDescription = "Account Group" },
                    new Group { GroupName = "Admin", GroupDescription = "Admin Group" }
                };
                await _context.Group.AddRangeAsync(groups);
                await _context.SaveChangesAsync();

                List<Product> products = new List<Product>() {
                    new Product{ProductName = "First Book About The Orchestra",UnitOfMeasureId=1,Balance=1000,Barcode="1",Description="book",DefaultBuyingPrice=4.64,DefaultSellingPrice=23.19,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "The Classical Music Book : Big Ideas Simply Explained",UnitOfMeasureId=1,Balance=1000,Barcode="2",Description="book",DefaultBuyingPrice=6.42,DefaultSellingPrice=32.12,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "My Mozart Music Book",UnitOfMeasureId=1,Balance=1000,Barcode="3",Description="book",DefaultBuyingPrice=4.16,DefaultSellingPrice=20.78,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "My Music Practice Book",UnitOfMeasureId=1,Balance=1000,Barcode="4",Description="book",DefaultBuyingPrice=1.35,DefaultSellingPrice=6.73,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Alfred's Music Manuscript Book : 10 Staves",UnitOfMeasureId=1,Balance=1000,Barcode="5",Description="book",DefaultBuyingPrice=1.54,DefaultSellingPrice=7.71,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "The Everything Music Theory Book ",UnitOfMeasureId=1,Balance=1000,Barcode="6",Description="book",DefaultBuyingPrice=5.37,DefaultSellingPrice=26.87,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Burgm ller, Czerny & Hanon",UnitOfMeasureId=1,Balance=1000,Barcode="7",Description="book",DefaultBuyingPrice=2.96,DefaultSellingPrice=14.8,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "The Music Tree Student's Book",UnitOfMeasureId=1,Balance=1000,Barcode="8",Description="book",DefaultBuyingPrice=2.5,DefaultSellingPrice=12.5,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "All About Music Theory",UnitOfMeasureId=1,Balance=1000,Barcode="9",Description="book",DefaultBuyingPrice=5.38,DefaultSellingPrice=26.92,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Harry Potter Music Writing Book",UnitOfMeasureId=1,Balance=1000,Barcode="10",Description="book",DefaultBuyingPrice=1.08,DefaultSellingPrice=5.41,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "The Movie Musicals Big Book : Piano/Vocal/Chords",UnitOfMeasureId=1,Balance=1000,Barcode="11",Description="book",DefaultBuyingPrice=5.37,DefaultSellingPrice=26.87,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Arban's Complete Conservatory Method for Trumpet ",UnitOfMeasureId=1,Balance=1000,Barcode="12",Description="book",DefaultBuyingPrice=7.07,DefaultSellingPrice=35.33,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "The Totally Awesome 80s Pop Music Trivia Book",UnitOfMeasureId=1,Balance=1000,Barcode="13",Description="book",DefaultBuyingPrice=4.83,DefaultSellingPrice=24.14,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Building Technic with Beautiful Music",UnitOfMeasureId=1,Balance=1000,Barcode="14",Description="book",DefaultBuyingPrice=2.14,DefaultSellingPrice=10.71,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Guitar Atlas Flamenco",UnitOfMeasureId=1,Balance=1000,Barcode="15",Description="book",DefaultBuyingPrice=4.31,DefaultSellingPrice=21.55,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Beethoven : Symphonies Nos. 5, 6 And 7",UnitOfMeasureId=1,Balance=1000,Barcode="16",Description="scores",DefaultBuyingPrice=6.29,DefaultSellingPrice=31.44,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "W.A. Mozart : Complete String Quartets",UnitOfMeasureId=1,Balance=1000,Barcode="17",Description="scores",DefaultBuyingPrice=6.18,DefaultSellingPrice=30.92,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Paganini/Wieniawski : Caprices And Etudes For Violin",UnitOfMeasureId=1,Balance=1000,Barcode="18",Description="scores",DefaultBuyingPrice=3.49,DefaultSellingPrice=17.44,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "The Firebird in Full Score (Original 1910 Version)",UnitOfMeasureId=1,Balance=1000,Barcode="19",Description="scores",DefaultBuyingPrice=6.18,DefaultSellingPrice=30.92,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Maurice Ravel : Daphnis And Chloe",UnitOfMeasureId=1,Balance=1000,Barcode="20",Description="scores",DefaultBuyingPrice=7.26,DefaultSellingPrice=36.32,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Joseph Haydn : String Quartets Opp. 20 And 33",UnitOfMeasureId=1,Balance=1000,Barcode="21",Description="scores",DefaultBuyingPrice=5.91,DefaultSellingPrice=29.57,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Antonin Dvorak : Symphonies Nos. 8 and 9",UnitOfMeasureId=1,Balance=1000,Barcode="22",Description="scores",DefaultBuyingPrice=6.18,DefaultSellingPrice=30.92,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "F.J. Haydn : Twelve String Quartets",UnitOfMeasureId=1,Balance=1000,Barcode="23",Description="scores",DefaultBuyingPrice=6.18,DefaultSellingPrice=30.92,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Gustav Mahler : Symphony No.6 In A Minor",UnitOfMeasureId=1,Balance=1000,Barcode="24",Description="scores",DefaultBuyingPrice=3.49,DefaultSellingPrice=17.44,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Schumann, Saint-Saens And Dvorak : Great Romantic Cello Concertos",UnitOfMeasureId=1,Balance=1000,Barcode="25",Description="scores",DefaultBuyingPrice=5.37,DefaultSellingPrice=26.87,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Ludwig Van Beethoven : Complete Violin Sonatas",UnitOfMeasureId=1,Balance=1000,Barcode="26",Description="scores",DefaultBuyingPrice=6.18,DefaultSellingPrice=30.92,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Johannes Brahms : The Complete Sonatas - Violin/Piano",UnitOfMeasureId=1,Balance=1000,Barcode="27",Description="scores",DefaultBuyingPrice=4.57,DefaultSellingPrice=22.84,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Franz Schubert : Four Symphonies",UnitOfMeasureId=1,Balance=1000,Barcode="28",Description="scores",DefaultBuyingPrice=5.9,DefaultSellingPrice=29.48,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Richard Wagner : Overtures And Preludes",UnitOfMeasureId=1,Balance=1000,Barcode="29",Description="scores",DefaultBuyingPrice=6.72,DefaultSellingPrice=33.62,BranchId=1,CurrencyId=1},
                    new Product{ProductName = "Tone Poems in Full Score : In Full Score",UnitOfMeasureId=1,Balance=1000,Barcode="30",Description="scores",DefaultBuyingPrice=7.26,DefaultSellingPrice=36.32,BranchId=1,CurrencyId=1},
                };
                await _context.Product.AddRangeAsync(products);
                await _context.SaveChangesAsync();

                await _context.CustomerType.AddAsync(new CustomerType { CustomerTypeName = "Normal" });
                await _context.SaveChangesAsync();

                List<Customer> customers = new List<Customer>() {
                    new Customer{CustomerName = "Hanari Carnes", Address = "Rua do Pa?o, 67", Email= "test0@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "HILARION-Abastos", Address = "Carrera 22 con Ave. Carlos Soublette #8-35", Email= "test1@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Hungry Coyote Import Store", Address = "City Center Plaza 516 Main St.", Email= "test2@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Hungry Owl All-Night Grocers", Address = "8 Johnstown Road", Email= "test3@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Island Trading", Address = "Garden House Crowther Way", Email= "test4@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "K?niglich Essen", Address = "Maubelstr. 90", Email= "test5@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "La corne d'abondance", Address = "67, avenue de l'Europe", Email= "test6@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "La maison d'Asie", Address = "1 rue Alsace-Lorraine", Email= "test7@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Laughing Bacchus Wine Cellars", Address = "1900 Oak St.", Email= "test8@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Lazy K Kountry Store", Address = "12 Orchestra Terrace", Email= "test9@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Lehmanns Marktstand", Address = "Magazinweg 7", Email= "test10@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Let's Stop N Shop", Address = "87 Polk St. Suite 5", Email= "test11@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "LILA-Supermercado", Address = "Carrera 52 con Ave. Bolívar #65-98 Llano Largo", Email= "test12@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "LINO-Delicateses", Address = "Ave. 5 de Mayo Porlamar", Email= "test13@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Lonesome Pine Restaurant", Address = "89 Chiaroscuro Rd.", Email= "test14@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Magazzini Alimentari Riuniti", Address = "Via Ludovico il Moro 22", Email= "test15@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Maison Dewey", Address = "Rue Joseph-Bens 532", Email= "test16@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Mère Paillarde", Address = "43 rue St. Laurent", Email= "test17@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Morgenstern Gesundkost", Address = "Heerstr. 22", Email= "test18@gmail.com",CustomerTypeId=1},
                    new Customer{CustomerName = "Old World Delicatessen", Address = "2743 Bering St.", Email= "test19@gmail.com",CustomerTypeId=1}
                };
                await _context.Customer.AddRangeAsync(customers);
                await _context.SaveChangesAsync();

                await _context.VendorType.AddAsync(new VendorType { VendorTypeName = "Normal" });
                await _context.SaveChangesAsync();

                List<Vendor> vendors = new List<Vendor>() {
                    new Vendor{VendorName = "Exotic Liquids", Address = "49 Gilbert St.",VendorTypeId=1},
                    new Vendor{VendorName = "New Orleans Cajun Delights", Address = "P.O. Box 78934",VendorTypeId=1},
                    new Vendor{VendorName = "Grandma Kelly's Homestead", Address = "707 Oxford Rd.",VendorTypeId=1},
                    new Vendor{VendorName = "Tokyo Traders", Address = "9-8 Sekimai Musashino-shi",VendorTypeId=1},
                    new Vendor{VendorName = "Cooperativa de Quesos 'Las Cabras'", Address = "Calle del Rosal 4",VendorTypeId=1},
                    new Vendor{VendorName = "Mayumi's", Address = "92 Setsuko Chuo-ku",VendorTypeId=1},
                    new Vendor{VendorName = "Pavlova, Ltd.", Address = "74 Rose St. Moonie Ponds",VendorTypeId=1},
                    new Vendor{VendorName = "Specialty Biscuits, Ltd.", Address = "29 King's Way",VendorTypeId=1},
                    new Vendor{VendorName = "PB Knäckebröd AB", Address = "Kaloadagatan 13",VendorTypeId=1},
                    new Vendor{VendorName = "Refrescos Americanas LTDA", Address = "Av. das Americanas 12.890",VendorTypeId=1},
                    new Vendor{VendorName = "Heli Süßwaren GmbH & Co. KG", Address = "Tiergartenstraße 5",VendorTypeId=1},
                    new Vendor{VendorName = "Plutzer Lebensmittelgroßmärkte AG", Address = "Bogenallee 51",VendorTypeId=1},
                    new Vendor{VendorName = "Nord-Ost-Fisch Handelsgesellschaft mbH", Address = "Frahmredder 112a",VendorTypeId=1},
                    new Vendor{VendorName = "Formaggi Fortini s.r.l.", Address = "Viale Dante, 75",VendorTypeId=1},
                    new Vendor{VendorName = "Norske Meierier", Address = "Hatlevegen 5",VendorTypeId=1},
                    new Vendor{VendorName = "Bigfoot Breweries", Address = "3400 - 8th Avenue Suite 210",VendorTypeId=1},
                    new Vendor{VendorName = "Svensk Sjöföda AB", Address = "Brovallavägen 231",VendorTypeId=1},
                    new Vendor{VendorName = "Aux joyeux ecclésiastiques", Address = "203, Rue des Francs-Bourgeois",VendorTypeId=1},
                    new Vendor{VendorName = "New England Seafood Cannery", Address = "Order Processing Dept. 2100 Paul Revere Blvd.",VendorTypeId=1}
                };
                await _context.Vendor.AddRangeAsync(vendors);
                await _context.SaveChangesAsync();

                List<OrderProgressType> ListOfOrderProgressType = new List<OrderProgressType>() {
                    new OrderProgressType{ OrderProgressTypeName="Drafting", Description="Drafting"},//1
                    new OrderProgressType{ OrderProgressTypeName="Confirmed", Description="Confirmed"},//2
                    new OrderProgressType{ OrderProgressTypeName="Approved", Description="Approved"},//3
                    new OrderProgressType{ OrderProgressTypeName="Shipping", Description="Shipping"},//4
                    new OrderProgressType{ OrderProgressTypeName="Closed", Description="Closed"},//5
                    new OrderProgressType{ OrderProgressTypeName="Cancelled", Description="Cancelled"},//6
                    new OrderProgressType{ OrderProgressTypeName="Redeemed", Description="Redeemed"},//7
                    new OrderProgressType{ OrderProgressTypeName="Packing", Description="Packing"},//8
                    new OrderProgressType{ OrderProgressTypeName="Printing", Description="Printing"},//9 for bulk order
                    new OrderProgressType{ OrderProgressTypeName="Balance Verified", Description="Balance Verified"}//10
                };
                await _context.OrderProgressType.AddRangeAsync(ListOfOrderProgressType);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SendEmailBySendGridAsync(string apiKey,
            string fromEmail,
            string fromFullName,
            string subject,
            string message,
            string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromFullName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email, email));
            await client.SendEmailAsync(msg);
        }

        public async Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL)
        {
            var body = messageBody;
            var message = new MailMessage();
            message.To.Add(new MailAddress(toEmail, toFullName));
            message.From = new MailAddress(fromEmail, fromFullName);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = smtpUser,
                    Password = smtpPassword
                };
                smtp.Credentials = credential;
                smtp.Host = smtpHost;
                smtp.Port = smtpPort;
                smtp.EnableSsl = smtpSSL;
                await smtp.SendMailAsync(message);
            }
        }

        public async Task CreateDefaultSuperAdmin()
        {
            try
            {
                await _roles.GenerateRolesFromPagesAsync();

                ApplicationUser superAdmin = new ApplicationUser();
                superAdmin.Email = _superAdminDefaultOptions.Email;
                superAdmin.UserName = superAdmin.Email;
                superAdmin.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(superAdmin, _superAdminDefaultOptions.Password);

                if (result.Succeeded)
                {
                    //add to user profile
                    UserProfile profile = new UserProfile();
                    profile.FirstName = "Super";
                    profile.LastName = "Admin";
                    profile.Email = superAdmin.Email;
                    profile.ApplicationUserId = superAdmin.Id;
                    profile.GroupId = 5;
                    profile.ProfilePicture = "/upload/admin.png";
                    await _context.UserProfile.AddAsync(profile);
                    await _context.SaveChangesAsync();

                    await _roles.AddToRoles(superAdmin.Id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateUsers(string email,string password, string firstName, string lastName, int groupId, List<string> roleNameList) {
            try
            {
                await _roles.GenerateRolesFromPagesAsync();

                ApplicationUser user = new ApplicationUser();
                user.Email = email;
                user.UserName = email;
                user.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    //add to user profile
                    UserProfile profile = new UserProfile();
                    profile.FirstName = firstName;
                    profile.LastName = lastName;
                    profile.Email = user.Email;
                    profile.ApplicationUserId = user.Id;
                    profile.GroupId = groupId;
                    profile.ProfilePicture = "/upload/" + groupId + ".png";
                    await _context.UserProfile.AddAsync(profile);
                    await _context.SaveChangesAsync();
                    foreach (string roleName in roleNameList) {
                        await _roles.AddRoleByName(user.Id, roleName);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env, string uploadFolder)
        {
            var result = "";

            var webRoot = env.WebRootPath;
            var uploads = System.IO.Path.Combine(webRoot, uploadFolder);
            var extension = "";
            var filePath = "";
            var fileName = "";

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    extension = System.IO.Path.GetExtension(formFile.FileName);
                    fileName = Guid.NewGuid().ToString() + extension;
                    filePath = System.IO.Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    result = fileName;
                }
            }

            return result;
        }
    }
}