# B3 Case
This is a case project for the selection process

## Setup

Install [SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0) ASP.Net Core 8

Install [Docker](https://docs.docker.com/desktop/install/windows-install/) 

### Get Start

In the console entry under B3Case\B3CaseDB and use the command

### RabbitMQ project

```bash
  docker run -d --hostname rabbitserver --name rabbitmq-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```
#### RabbitMQ Credentials
  Open [Localhost](http://localhost:15672/) 
##### Login:
  - Email: guest
  - Password: guest

### Database project

```bash
  docker-compose up -d

  docker container ls

  docker inspect "CONTAINER ID" for image postgres
```

Open [Localhost](http://localhost:8080/) 

#### Backend project

Open project B3Case.sln and run project

### Database Credentials

#### Login PgAdmin:
  - Email: admin@b3case.com
  - Password: B3C@se

#### Adding new server:

##### In General:
- Name: B3_Case

##### In Connection:
- Host name/address: Use gateway IP to inspect return docker
- Username: sa
- Password: B3C@se

### Project Execution Guide

- Create order
  ```bash
    Post - /api/order/Order
    Request
            {
              "description": "string",
              "date": "2024-08-26T11:36:30.559Z"
            }
  ```