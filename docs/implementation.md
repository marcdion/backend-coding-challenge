# Implementation notes

This application was implemented using a microservices architecture. I decided to go this route as it is a much more scalable approach as both parts of the application can be improved and maintained without compromising the other. (I am aware that a true microservice architecture would split the code into seperate code bases but they we're kept in the same repository for the sake of this assignment).

Vue Js was selected as the front end framework since it is very easy framework to work with and the performance is great. The front end was developed with a fully responsive approach, meaning that it is accessible on all types of devices. Bootstrap and mixins we're used to accomplish this. We communicate with the API through AJAX with Axios and i18n was implemented to support localization (French and english support). Everything is being tested with Test café in TypeScript and it is built and deployed on Netlify.

The API was built using the latest version of .NET Core (3.1). The API was implemented using the RESTful approach. Multiple versions of the API have been implemented to increase maintainabilty in the future. The API version is passed via the request headers. Documentation has been provided and implemented using Swagger and the Swashbuckle NuGet package. The endpoint parameters are fully validated, edge cases have been tested and consistent validation errors are provided via an Error Handling module. See [ADR-003](./adr/ADR-003.md) to get more information on why I decided to use a Trie (prefix tree) data structure.

The API is deployed with AppVeyor and Web deploy and hosted on Azure. Since the Azure plan is free, it tends to shutdown when no traffic is present. To fix this issue, I itegrated the Uptime Robot SASS which does two things. First off, it informs me of any issues with both applications (makes sure nothing is down). By doing this, it is poking the API every five minutes at the /status endpoint. This assures that the API is always up and ready to receive a query.

Some of my decision have been documented in the ADR documentation. [See here.](./adr/contents.md)

I would of liked to have included more features but I unfortunately ran out of time. You can read more about it [here.](./improvements.md)