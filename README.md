## Microservices for User Information and Face Recognition

![recognizer](https://github.com/user-attachments/assets/c0d90441-61b6-4383-a074-a39c98a60d5b)

#### Microservices Architecture
* We’ve adopted a microservices architecture, which breaks down the application into loosely coupled, fine-grained services. Each service runs independently, communicating through lightweight protocols1.
* The goal is to enhance scalability, maintainability, and deployment flexibility.

#### Microservices Communication
* Sync inter-service **gRPC Communication**

#### Face Recognition Microservice (Python):
* Written in Python, this service handles face recognition tasks.
* It processes photos and identifies whether the subject is human.
* Leveraging Python’s libraries (such as OpenCV or dlib), it extracts facial features and matches them against known profiles.

#### Display Layer Microservice (ASP.NET Core Razor Pages):
* Developed using ASP.NET Core Razor Pages, this service provides the user interface.
* It renders web pages, interacts with users, and communicates with other services.
* Razor Pages allow efficient development of dynamic web content.

#### Database Service (PostgreSQL):
* We use PostgreSQL as our database system.
* It stores user data, including profile information and face recognition results.
* PostgreSQL’s reliability and support for complex queries make it an excellent choice.
