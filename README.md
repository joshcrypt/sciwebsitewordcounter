# SCI Website Word Counter
### The best method of storing keys
The best way for me is to use the Azure Key Vault and deploying applications to Azure App service.
This is because one can store secrets and limit access to known administrators.
Azure Key Vault allows someone to encrypt application secrets in a secure way

# Purpose of this application
This purpose of this application is to perform a word count of the number of words contained in a WebSite and arranging them in descending order.
The application also updates and inserts the content obtained from the website in a Google Cloud Sql Instance.
This application has been created using the TempData dictionary as shown
```sh
TempData["wordcountdata"] = wordcount;
```
and displayed as shown
```sh
@foreach (var wcd in (IEnumerable<WordCount>)TempData["wordcountdata"])
    {
        <tr>
            <td style ="font-size:"@(wcd.SiteCount)+"px">@wcd.SiteKey</td>
            <td style ="font-size:"@(wcd.SiteCount)+"px">@wcd.SiteCount</td>
        </tr>
    }
```
### Setup
This application has been setup and published using the following services
* [Azure App Service] - This application has been configured and deployed to Azure APP service and can be accessed via this URL http://sciwebsitewordcounter20191214011235.azurewebsites.net/
* [Google Cloud SQL] - The application's backend is setup using Google Cloud Sql leveraging on the managed instance of MYSQL in the cloud. The application is hosted in the Google Cloud Platform.
* [Twitter Bootstrap] - This application has been built using Bootstrap
* [asp.net core] - the application has been built using the asp.net core framework

### Usage
Navigate to this url in order to access the application
http://sciwebsitewordcounter20191214011235.azurewebsites.net/
on the text box insert the https url and press Submit

The results should show on the next window.

The application has also been configured to insert in google cloud
The application has been configured to block out common prepositions and two word characters.
```sh
string[] blockedprepositions = { "and", "for", "is", "on", "or", "to", "the","a" };
```
### The best method of storing keys
The best way for me is to use the Azure Key Vault and deploying applications to Azure App service.
This is because one can store secrets and limit access to known administrators.
Azure Key Vault allows someone to encrypt application secrets in a secure way
