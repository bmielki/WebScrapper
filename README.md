# WebScrapper

## The Project
This is a WebScrapper program. It allows the user to extract information from a webpage through it's URL.
It features 2 extractions:
* Image extraction - Extracts all images from a website.
* Word extraction - Extracts the 10 most common words in the website

## Built with
* C# .NET - MVC structure (https://visualstudio.microsoft.com/pt-br/vs/community/)
* Agility pack - C# lib (https://html-agility-pack.net/download)

## Contact
Bernardo Mielki
* Linkedin (https://www.linkedin.com/in/bmielki/)
* Email (bernardo_fm@hotmail.com)

## How to run
1) Clone the repository
$ git clone https://github.com/bmielki/WebScrapper.git

2) Open the solution using Visual Studio and Run

## Project detailed description
The project consist in 2 controllers and 3 views.
* The first controller (Home) is responsible for collecting the URL in the
main view (Index) and also checking if the inserted URL is valid.
* Once the validation is complete, the Home controller passes the UrlModel (with the valid URL)
for the second controller.
* The second controller (Scrapper) is then responsible for collection the images used
on the URL as well as the common words used in text.
* For that, it uses the library HtmlAgilityPack with navigates through the html nodes
of the page, searching for specific tags.
* For the image collection, it looks for the "src" in the <img> tags. And for the word
collection, it search for text inputs (inner text).

## Further development
* The words scrapper could use some tunning, and deserves further development. 
Once the text can be handled and rendered in the JavaScript files of the website,
sometimes the scrapper will collect variable names istead of the proper text in the
content.
