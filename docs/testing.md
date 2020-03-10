# Testing

Front end tests are run with TestCafé and are written in TypeScript. You can run them locally with the following command if you have TestCafé installed. Keep in mind you have to be in the **suggestion-front-end** directory: 

 ``` npm run test ```

The aim of this test suite is to validate basic functionnalities of the application! These tests are not run in the CI process because it is complicated to run intergration tests outside of Docker in the CI!

API tests are built with xUnit and Shouldly. The aim of the unit tests it to verify that the different blocks work on their own (Classes, Extensions, Data storing). The interactions between the different services are not being tested for two reasons. First off, they are arduous to create because of the dependency injection mocking and they can take a long time to execute. The second reason is that the interactions and end result of a query are is in the front end integration tests. Learn more [here.](./run-locally.md)