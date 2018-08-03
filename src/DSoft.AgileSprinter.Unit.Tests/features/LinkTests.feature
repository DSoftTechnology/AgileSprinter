Feature: Test Web Links
In order to navigate the web app
it is necessary for all the links to be correct and not broken

Scenario Outline: Test Links on Splash Page
	
	Given The splash page is loaded
	When user clicks "<link>"
	Then The web app redirects to new "<link>"
Examples: 
| link |
| Add a Controller and View                 |
| Manage User Secrets using Secret Manager. |
| Use logging to log a message.             |

Scenario Outline: Test Links on Entity Framework Page
	
	Given The Entity Framework page is loaded
	When user clicks "<link>"
	Then The web app redirects to new "<link>"
Examples: 
| link          |
| Razor Pages   |
| IIS Express   |
| Rick Anderson |
