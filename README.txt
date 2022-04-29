INSTRUCTIONS
------------
Hello Greta from Skövde! This readme will help you add Weavy to your existing Web Application!


DemoHostComplete is the "completed" project. The instructions below will need to be carried out to add Weavy to the DemoHost project.

############################################################################################################################################################################
# 1. Run site without Weavy.
############################################################################################################################################################################

- "Open Folder..." "\msbuilddemo\DemoHost" in VS Code.
- In VS Code "Terminal" -> "New Terminal"
- Navigate to \DemoHost
- Enter "dotnet run"
- Open a browser and navigate to the URL indicated by the logs in the terminal. E.g. https://localhost:7059 (use the HTTPS one!)
- Sign in and see the "existing" site without Weavy.
- To stop the site, enter CTRL+C in the terminal.

Cookie authentication is enabled on the site, but you can login as anyone with any password. Each different login will create a new user in Weavy when the dropin is loaded.


############################################################################################################################################################################
# 2. Add Weavy
############################################################################################################################################################################

You can take a look or diff the files from the DemoHostComplete project to see the differences. You can add the parts in any order.

Add identity provider for Weavy 
-------------------------------

- Open \msbuilddemo\DemoHost\appsettings.json
- Add below and save:

{
  "ClientId": "get.weavy.io",
  "ClientSecret": "RJ8gYBFgUqrfoDGfVnbB7G9NqjFCod7d"
}

(THESE ARE LIVE KEYS. JUST SO YOU KNOW...)

- The client secret is for the Weavy instance https://rickardstelefon.weavy.io/ for now.

- Copy the file "\msbuilddemo\DemoHostComplete\Controllers\TokenController.cs" ->  "\msbuilddemo\DemoHost\Controllers\TokenController.cs"

TokenController has an endpoint /token that returns a valid token for the current logged in user using the client you defined above.


Add markup (Messenger button with badge + container)
----------------------------------------

- Add below to "\msbuilddemo\DemoHost\Views\Home\Index.cshtml" (just after <a class="navbar-brand" asp-controller="home" asp-action="index"><img src="~/favicon.svg" width="24" height="24" /></a>)

<div class="navbar-nav flex-row">
    <button id="messenger" class="btn btn-icon">
        <svg height="24" viewBox="0 0 24 24" width="24" class="i i-weavy-messenger text-white">
        <path d="m2 5.244c0-1.784 1.46-3.244 3.244-3.244h13.511c1.785 0 3.245 1.46 3.245 3.244v10.178c0 1.784-1.46 3.244-3.244 3.244h-13.078l-3.678 3.334zm13.089.789-.809 1.401 1.578 2.734 1.598-2.768c.378-.655.154-1.49-.5-1.867-.215-.125-.45-.184-.681-.184-.474 0-.933.245-1.186.684zm-1.453 6.941c.095.166.22.304.365.411.079.061.165.111.255.151.092.042.19.074.289.094.091.019.183.028.275.028.232 0 .468-.059.683-.184.653-.377.877-1.213.5-1.867l-2.81-4.865c-.254-.439-.712-.684-1.184-.684-.233 0-.468.059-.683.184-.655.377-.879 1.213-.5 1.867zm.066.929c-.015-.01-.028-.021-.042-.031-.217-.159-.402-.362-.54-.601l-1.106-1.913-.003.006-1.618 2.802c-.049.083-.088.171-.116.258-.052.157-.074.319-.067.478.018.453.26.888.683 1.131.215.125.45.184.683.184.235 0 .468-.061.672-.177.184-.104.347-.252.472-.441.015-.022.982-1.696.982-1.696zm-7.158-6.544 3.546 6.145 1.579-2.735-2.758-4.777c-.253-.438-.712-.685-1.186-.685-.232 0-.466.059-.681.184-.654.378-.879 1.215-.5 1.868z">
        </path>
        </svg>
    </button>
</div>

- Add below (just after the <nav> element)

<div id="drawer" class="drawer"></div>


Add the client scripts
----------------------

- In the terminal in VS Code make sure you are in \msbuilddemo\DemoHost.
- Run "npm install @weavy/dropin-js"
- Copy the files in the folder: \msbuilddemo\DemoHost\node_modules\@weavy\dropin-js\dist
- Drop the "weavy-dropin.js" file in \msbuilddemo\DemoHost\wwwroot\js
- Drop the "weavy-dropin.css" file in \msbuilddemo\DemoHost\wwwroot\css
- Add below to the <head> element in \msbuilddemo\DemoHostComplete\Views\Home\Index.cshtml (before script.js is loaded)

<link href="~/css/weavy-dropin.css" rel="stylesheet" asp-append-version="true">  
<script src="~/js/weavy-dropin.js" asp-append-version="true"></script>

In order not to mix in build steps I suggest, for simplicity, just to drop the files in the wwwroot. Can mention that the JS normally is imported and included in the build process like so: import Weavy from "@weavy/dropin-js";

Load the Weavy client
---------------------

Copy the contents of "\msbuilddemo\DemoHostComplete\wwwroot\js\script.js" -> "\msbuilddemo\DemoHost\wwwroot\js\script.js"

############################################################################################################################################################################
# 3. Test
############################################################################################################################################################################

- Navigate to \DemoHost in the terminal in VS Code
- Enter "dotnet run"
- Open a browser and navigate to the URL indicated by the logs in the terminal. E.g. https://localhost:7059 (use the HTTPS one!)
- Sign in and see the "existing" site WITH Weavy.
- To stop the site, enter CTRL+C in the terminal.



