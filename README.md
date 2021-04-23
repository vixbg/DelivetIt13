# DeliverIt13
This is the repository for the ASP.NET Core project of Team13. 
Team members are Victor Vasilev and Rumyana Damyanova. 
The project is a Freight Forwarding Management System.

In order to build and run the application do the following:
1. Pull from Master
2. Go to DeliverIt13.Web => appsettings.json => appsettings.Development.json => appsettings.Development.
3. Run.json and edit it and input your database string.
4. Open Package Management Console and add initial migration on DeliverIt13.Data. 
5. Start the application from DeliverIt13.Web.Run in VS2019. This will create the database and seed data automatically. Note - update-database is not working at the moment. There is an automatic migration created if changes occur in the .Data models. 

Link to the Trello board [here](https://trello.com/b/Ny30Q0Rz). 

Link to Swagger Documentation (Swagger starts automatically) - [here](http://localhost:5000/swagger/index.html)

Database and Class Diagrams are uploaded in Trello.
