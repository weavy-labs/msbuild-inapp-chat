INSTRUCTIONS
------------

DemoHostComplete is the "completed" project. The instructions below will need to be carried out to add Weavy to the DemoHost project.

1. Run site without Weavy.
    - Open folder "DemoHost" in VS Code.
    - In VS Code "Terminal" -> "New Terminal"
    - Navigate to \DemoHost
    - Enter "dotnet run"
    - Open a browser and navigate to the URL indicated by the logs in the terminal. E.g. https://localhost:7059
    - Sign in and see the "existing" site.
    - To stop the site, enter ctrl+c in the terminal.

Cookie authentication is enabled on the site, but you can login as anything with any password. Each different login will create a new user in Weavy.    

2. Add Weavy

You can take a look or diff the files from the DemoHostComplete project to see the differences. You can add the parts in any order.

Add the client scripts
---------------

    - In the terminal in VS Code make sure you are in \DemoHost.
    - Run "npm install @weavy/dropin-js"
    - Copy the files in the folder: \msbuilddemo\DemoHost\node_modules\@weavy\dropin-js\dist
    - Drop the "weavy-dropin.js" file in \msbuilddemo\DemoHost\wwwroot\js
    - Drop the "weavy-dropin.css" file in \msbuilddemo\DemoHost\wwwroot\css
    - Add below to the <head> element in \msbuilddemo\DemoHostComplete\Views\Home\Index.cshtml (before script.js is loaded)

<link href="~/css/weavy-dropin.css" rel="stylesheet" asp-append-version="true">  
<script src="~/js/weavy-dropin.js" asp-append-version="true"></script>






In order not to mix in build steps I suggest, for simplicity, just to drop the files in the wwwroot. Can mention that the JS normally is imported and added to the build steps like so: import Weavy from "@weavy/dropin-js";