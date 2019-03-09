## SECTION 1 : DoReMi online Order Ordering system
(Includes Inventory Control and Management)


<img src="Media/Images/login_screen.jpg"
     style="float: left; margin-right: 0px;" />



<img src="Media/Images/OrderProcessFlow.jpg"
     style="float: left; margin-right: 0px; width: 1300px; height: 900px;" />

---
## SECTION 2 : EXECUTIVE SUMMARY / PAPER ABSTRACT
DoReMi Books Inc. is a company started in 1955 (and is now 57 years old) and specializes in the supply and sale of classical music scores and music books in the USA with subsidiaries in the major cities of each of the 50 states. As a traditional industry, some of its business processes are manual and rather traditional. With the rapid development of the information technology (IT), especially the fast popularize and spread of Internet technology, the company is now facing strong competition from its competitors (both new and old), who have embraced online and internet sales as the new way of interacting and transacting with their customers.

In response to the new challenges brought on by online sales transactions, the company decided to conduct a business process improvement exercise to revamp their music books sale transaction and order handling process as well as introduce improved stock and inventory planning and management capabilities. This will include a new order handling process, an internet sales transaction system and an advanced business intelligence module for optimal stock inventory and warehousing forecasting.

Based on machine reasoning concepts and how logical rules and knowledge can be inferred with a reasoning based system, our group of five members, brainstormed and decided to build a system with hybrid architecture (NRules Engine and Web MVC technology) and automate, as much as we can, the business or functions based on the knowledge gleaned from: (a) feedback from the management board,  (b) current order handling processes and, (c) business issues encountered in current existing process. 

We set out first by getting a good understanding of the overall key processes of the proposed solution by mapping them out with data acquisition modelling. We then determined the areas of improvements that we agreed should be implemented into the new business processes. We also make sure we analyse the processes with our improvements and see how these improvements would be handled with test cases to validate our assumptions.

Our primary objective is to solve the business problems which are typically encountered in many traditional businesses which naturally includes DoReMi’s books ordering business processes.


---
## SECTION 3 : CREDITS / PROJECT CONTRIBUTION

| Official Full Name  | Student ID (MTech Applicable)  | Work Items (Who Did What) | Email (Optional) |
| :------------ |:---------------:| :-----| :-----|
| NG CHOON BENG | A0195327X | xxxxxxxxxx yyyyyyyyyy zzzzzzzzzz| e0384958@u.nus.edu |
| JIN XIN | A0066966L | xxxxxxxxxx yyyyyyyyyy zzzzzzzzzz| e0384232@u.nus.edu |
| XU DONGBIN | A0018636A | xxxxxxxxxx yyyyyyyyyy zzzzzzzzzz| e0384187@u.nus.edu |
| SUN HANG | A0105742M | xxxxxxxxxx yyyyyyyyyy zzzzzzzzzz| e0384337@u.nus.edu |
| LI XIN | A0132084N | xxxxxxxxxx yyyyyyyyyy zzzzzzzzzz| e0384426@u.nus.edu |

---
## SECTION 4 : VIDEO OF SYSTEM MODELLING & USE CASE DEMO

[![online music books ordering system](http://img.youtube.com/vi/-AiYLUjP6o8/0.jpg)](https://youtu.be/-AiYLUjP6o8 "Sudoku AI Solver")

---
## SECTION 5 : USER GUIDE

`<Github File Link>` : <https://github.com/chadng/MTech2019_Group_Project>

### Development Tools & Environment
- **Visual Studio 2017 (Community Edition)**. (https://visualstudio.microsoft.com/) 
- **Syncfusion JQuery Controls EJ1 (Community Edition)**. (https://www.syncfusion.com/products/communitylicense)

### Running Locally
* Download Visual Studio 2017 and install .Net development
* Clone this repo
* Open doremi.sln and make sure you have selected doremi
* click IIS Express
* ![run](doremi/wwwroot/images/run.png)

### Live Version
Project is also released in azure: https://doremiwebapp.azurewebsites.net

### Credit
[Inventory order management system by go2ismail](https://github.com/go2ismail/Asp.Net-Core-Inventory-Order-Management-System)

---
## SECTION 6 : PROJECT REPORT / PAPER

`<Github File Link>` : <https://github.com/chadng/MTech2019_Group_Project/blob/Readme/ProjectReport/Project%20Report%20HDB-BTO.pdf>

**Recommended Sections for Project Report / Paper:**
- EXECUTIVE SUMMARY
- PROBLEM DESCRIPTION
- KNOWLEDGE MODELING
- SOLUTION OUTLINE
- CONCLUSION & REFERENCES
- BIBLIOGRAPHY
- APPENDIX A: SAMPLE INPUT & SYSTEM OUTPUT
- APPENDIX B: USERS MANUAL
- APPENDIX C: TECHNICAL SPECIFICATIONS

---
## SECTION 7 : MISCELLANEOUS

### 123
* 123

---































### Worksop Project Submission Template: Github Repository & Zip File  

**[Naming Convention]** CourseCode-StartDate-BatchCode-Group_or_Individual-TeamName_or_PersonName-ProjectName.zip

* **[MTech Group Project Naming Example]** IRS-MR-2019-01-19-IS1PT-GRP-AwsomeSG-HDB_BTO_Recommender.zip

* **[MTech Individual Project Naming Example]** IRS-MR-2019-07-01-IS1FT-IND-SamGuZhan-HDB_BTO_Process.zip

* **[EEP Group Project Naming Example]** IRS-MR-2019-03-13-EEP-GRP-AwsomeSG-HDB_BTO_Recommender.zip

* **[EEP Individual Project Naming Example]** IRS-MR-2019-08-22-EEP-IND-SamGuZhan-HDB_BTO_Process.zip

[Online editor for this README.md markdown file](https://pandao.github.io/editor.md/en.html "pandao")

---

### <<<<<<<<<<<<<<<<<<<< Start of Template >>>>>>>>>>>>>>>>>>>> Test

---

## SECTION 1 : PROJECT TITLE



---
## SECTION 2 : EXECUTIVE SUMMARY / PAPER ABSTRACT
Singapore ranks amongst countries with the highest population density in the world. In a bid to have firm control over long term urban planning, the Singapore government came up with the “Built to Order” (abbreviated BTO) initiative back in 2001. These are new Housing Development Board (HDB) flats tightly controlled by their eligibility and quantity released every year. In more recent years, the modern BTO scheme in Singapore requires a waiting period of 3-4 years, and is generally targeted at young Singaporean couples looking to purchase their first property and set up a family. Nationality and income ceilings are some of the broad filters that determine one’s eligibility for the highly sought after projects. 


Our team, comprising of 6 young Singaporeans, all hope to be property owners one day. Many of our peers opt for BTO flats due to their affordability, existence of financial aid from the government, as well as their resale value. However, there often exists a knowledge gap for these young couples during the decision making process and they end up making potentially regretful decisions. We would like to bridge this knowledge gap, and have hence chosen to base our project on creating a recommender system for BTO flats, utilizing the data from recent launches in Tampines, Eunos, Sengkang and Punggol. 


Using the techniques imparted to us in lectures, our group first set out to build a sizeable knowledge base via conducting an interview and administering a survey. While building the system, we utilized tools such as Java to scrape real time data from HDB website and transform it into a database, CLIPS to synthesize the rule based reasoning process, and Python to integrate it into an easy to use UI for the everyday user. To add icing on the cake, we even hosted the system on a website so that the everyday user can access it through the click of a link.


Our team had an amazing time working on this project, and hope to share our insights with everyone. Despite a focus on BTO flats, we would recommend it for everybody interested in understanding property market trends for residence or investment purposes. There truly are a wide array of factors behind the decision to invest in a property, and we only wish there was more time to work on the scope and scale of the project. 

---
## SECTION 3 : CREDITS / PROJECT CONTRIBUTION

| Official Full Name  | Student ID (MTech Applicable)  | Work Items (Who Did What) | Email (Optional) |
| :------------ |:---------------:| :-----| :-----|
| Desmond Chua | A1234567A | xxxxxxxxxx yyyyyyyyyy zzzzzzzzzz| A1234567A@nus.edu.sg |
| Chang Ye Han | A1234567B | xxxxxxxxxx yyyyyyyyyy zzzzzzzzzz| A1234567B@gmail.com |
| Chee Jia Wei | A1234567C | xxxxxxxxxx yyyyyyyyyy zzzzzzzzzz| A1234567C@outlook.com |
| Ganesh Kumar | A1234567D | xxxxxxxxxx yyyyyyyyyy zzzzzzzzzz| A1234567D@yahoo.com |
| Jeanette Lim | A1234567E | xxxxxxxxxx yyyyyyyyyy zzzzzzzzzz| A1234567E@qq.com |

---
## SECTION 4 : VIDEO OF SYSTEM MODELLING & USE CASE DEMO

[![Sudoku AI Solver](http://img.youtube.com/vi/-AiYLUjP6o8/0.jpg)](https://youtu.be/-AiYLUjP6o8 "Sudoku AI Solver")

Note: It is not mandatory for every project member to appear in video presentation; Presentation by one project member is acceptable. 
More reference video presentations [here](https://telescopeuser.wordpress.com/2018/03/31/master-of-technology-solution-know-how-video-index-2/ "video presentations")

---
## SECTION 5 : USER GUIDE

`<Github File Link>` : <https://github.com/telescopeuser/Workshop-Project-Submission-Template/blob/master/UserGuide/User%20Guide%20HDB-BTO.pdf>

### [ 1 ] To run the system using iss-vm

> download pre-built virtual machine from http://bit.ly/iss-vm

> start iss-vm

> open terminal in iss-vm

> $ git clone https://github.com/telescopeuser/Workshop-Project-Submission-Template.git

> $ source activate iss-env-py2

> (iss-env-py2) $ cd Workshop-Project-Submission-Template/SystemCode/clips

> (iss-env-py2) $ python app.py

> **Go to URL using web browser** http://0.0.0.0:5000 or http://127.0.0.1:5000

### [ 2 ] To run the system in other/local machine:
### Install additional necessary libraries. This application works in python 2 only.

> $ sudo apt-get install python-clips clips build-essential libssl-dev libffi-dev python-dev python-pip

> $ pip install pyclips flask flask-socketio eventlet simplejson pandas

---
## SECTION 6 : PROJECT REPORT / PAPER

`<Github File Link>` : <https://github.com/telescopeuser/Workshop-Project-Submission-Template/blob/master/ProjectReport/Project%20Report%20HDB-BTO.pdf>

**Recommended Sections for Project Report / Paper:**
- Executive Summary / Paper Abstract
- Sponsor Company Introduction (if applicable)
- Business Problem Background
- Project Objectives & Success Measurements
- Project Solution (To detail domain modelling & system design.)
- Project Implementation (To detail system development & testing approach.)
- Project Performance & Validation (To prove project objectives are met.)
- Project Conclusions: Findings & Recommendation
- List of Abbreviations (if applicable)
- References (if applicable)

---
## SECTION 7 : MISCELLANEOUS

### HDB_BTO_SURVEY.xlsx
* Results of survey
* Insights derived, which were subsequently used in our system

---

### <<<<<<<<<<<<<<<<<<<< End of Template >>>>>>>>>>>>>>>>>>>>

---

**This [Machine Reasoning (MR)](https://www.iss.nus.edu.sg/executive-education/course/detail/machine-reasoning "Machine Reasoning") course is part of the Analytics and Intelligent Systems and Graduate Certificate in [Intelligent Reasoning Systems (IRS)](https://www.iss.nus.edu.sg/stackable-certificate-programmes/intelligent-systems "Intelligent Reasoning Systems") series offered by [NUS-ISS](https://www.iss.nus.edu.sg "Institute of Systems Science, National University of Singapore").**

**Lecturer: [GU Zhan (Sam)](https://www.iss.nus.edu.sg/about-us/staff/detail/201/GU%20Zhan "GU Zhan (Sam)")**

[![alt text](https://www.iss.nus.edu.sg/images/default-source/About-Us/7.6.1-teaching-staff/sam-website.tmb-.png "Let's check Sam' profile page")](https://www.iss.nus.edu.sg/about-us/staff/detail/201/GU%20Zhan)

**zhan.gu@nus.edu.sg**
