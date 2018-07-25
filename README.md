# Lawn Paynes

## Purpose
This iteration of the LawnPaynes webpage is an advancement of a proposal page for a friend's lawn care business. It expands on the 
previous iteration of that project [https://ryansallee.github.io](https://ryansallee.github.io/) by using EF and the ASP MVC .NET 
to build a Customer Manager that allows the site owner to add customers, locations for those customers, a list of services that can
be associated with a customer's location. As well, customers can use the Contact Form to submit their basic information to inquire 
about being a customer or an existing customer can submit changes to their information.

## Instructions

1. Clone the project from GitHub and run via Visual Studio (Open in Visual Studio link when cloning project).
2. Use NuGet Package Manager Console (Tools>NuGet Package Manager>Package Manager Console) and seed the Lawn Paynes database with
test data by using the command update-database.
3. The default page for the project is the customer-facing Home section's Index page. Interested customers can add their information
to the Lawn Paynes' database via the Contact Us page. As well, an existing customer's information in the database can be updated as long
as the entry matches a Customer Name and Email or Customer Name and Phone Number combination. To see the update in action, an example customer
that is seeded is as follows:
	1. Name: John Doe
	2. Phone Number: (345)-678-1234
	3. Email: john@google.com
	4. Comments: Test a comment!

4. To use all of the CRUD functions of the site, please navigate to the link entitled "Cool Stuff" at the bottom of the footer
as this link takes a user to the administrative portion of the site. A future enhancement to the site would be to add authorization 
to the site before it would be deployed to a production environment.