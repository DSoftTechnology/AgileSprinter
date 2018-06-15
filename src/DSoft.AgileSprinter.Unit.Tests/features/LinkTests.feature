Feature: Test Web Links
In order to navigate the web app
it is necessary for all the links to be correct and not broken

Scenario Outline: Test Links on Splash Page
Given The splash page is loaded
When user clicks the <link> web link
Then The web app redirects to new uri

Examples: 
|link|
| Add a Controller and View					|
| Manage User Secrets using Secret Manager. |
| Use logging to log a message.             |
