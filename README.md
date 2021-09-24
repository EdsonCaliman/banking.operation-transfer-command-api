# banking.operation-transfer-command-api

Banking Operation Solution - Transfer Command Api

[![.NET](https://github.com/EdsonCaliman/banking.operation-transfer-command-api/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/EdsonCaliman/banking.operation-transfer-command-api/actions/workflows/dotnet.yml)
[![Coverage Status](https://coveralls.io/repos/github/EdsonCaliman/banking.operation-transfer-command-api/badge.svg?branch=main)](https://coveralls.io/github/EdsonCaliman/banking.operation-transfer-command-api?branch=main)

This project is a part of a Banking Operation solution, with DDD, CQRS and microservices architecture, using .Net Core.

![BankingOperations (1)](https://user-images.githubusercontent.com/19686147/133843637-85277ee1-9748-4456-befa-4b2265e3ebec.jpg)

Using a docker-compose configuration the components will be connected so that together they work as a solution.

This component will be responsible for register the transfer and make the credit and debit entries in the transaction microservice. It uses a mysql transfer database to get the data.

![image](https://user-images.githubusercontent.com/19686147/134728766-3ba6612a-f942-4268-96fd-d7f1309c9bbb.png)

# Bussiness Rules

 - The request needs to have a valid client.
 - The request needs to have a valid contact.
 - The origin client needs to have enough balance.
 - The value needs to be between 0.1 and 10000.

# How to run

With a docker already installed:

Run first the projects: 

https://github.com/EdsonCaliman/banking.operation-client-api

https://github.com/EdsonCaliman/banking.operation-contact-api

https://github.com/EdsonCaliman/banking.operation-balance-query-api

https://github.com/EdsonCaliman/banking.operation-transaction-api

After run:

docker-compose up -d

For swagger open the URL: http://localhost:8004/swagger

![image](https://user-images.githubusercontent.com/19686147/134729677-78f6847c-7774-4a97-9a85-d40472ec6fef.png)


