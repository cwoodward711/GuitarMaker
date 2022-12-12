## Authors
Connor Woodward
Josh Wood
Edward Pazdziora
Mukesh mani Tripathi

## Name
GuitarMaker

## Description
This program allows you to build a custom guitar using a predefined list of parts. The GUI is generated dynamically from the .json file. The prices for these parts are retrieved through an API (which is currently not accurate).

## Usage
The project uses .NET 5.0. A Resources folder containing all the images used (in their own named folders) should be located in (projectRoot)/GuitarMaker/GuitarMaker in order for the program to display properly. There should also be a PartsList.json file inside the same Resources folder (no subfolder).

The user can click on the various buttons and tabs to change their guitar to their liking. The parts list on the right and prices associated are automatically updated as they do so. If a user would like to save a guitar configuration to a file for later use they may do so using the drop down menu at the top. This file can then be loaded using the load button, it will replace the current guitar configuration. If they would like to export the guitar to a text file, with associated prices, they may do so using the same menu.

## Bugs
The prices retrieved from the API are entirely inaccurate but getting real data from an API would cost money.

## Testing
No special instructions for running tests. Test coverage was determined using the extension Fine Code Coverage. Test cases did not include anything involving the GUI or API.
