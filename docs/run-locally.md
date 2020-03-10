# Run code locally
Since the application was developed using a microservices architecture, you have to run the front-end and the back end separatly.

### API
To run the API, you first need to make sure you have the latest version of the .NET Core SDK installed. You then simply have to be in the **/api/src** directory and use the following command:

 ``` dotnet run ```

 To run the tests, execute the following command while being in the **/api/tests/SuggestionApi.Tests** directory and run the following command:

  ``` dotnet test ```

 ### Front end
 To run the front end, you first need to make sure you have NPM and node installed. You then need to be in the **/suggestion-front-end** directory and run the following command:

``` npm install ```

And then run:

``` npm run serve ```

To run the front end tests, you first need to make sure both the front end and the API is running. You the need to be in the **/suggestion-front-end** directory and run the following command:

  ``` npm run test ```

