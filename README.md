# INSTRUCTIONS

Instructions on how to get in-app chat up and running in your .NET app using the Weavy Drop-in.

## Set up your sandbox

- Go to https://www.weavy.com/signup to set up your account and provision the sandbox
- When sandbox is provisioned, you'll find all the details you need under the "Account" tab in the top navbar

## Install the dropin-js

- Open a terminal window in the `DemoHost` folder
- Run `npm install @weavy/dropin-js`
- Copy the files in the folder: `DemoHost\node_modules\@weavy\dropin-js\dist`
- Drop the `weavy-dropin.js` file in `DemoHost\wwwroot\js`
- Drop the `weavy-dropin.css` file in `DemoHost\wwwroot\css`

## Add credentials for your sandbox

- Open `DemoHost\appsettings.json`
- Update with your ClientID and Secret below and save;

```json
{
  "ClientId": "{ YOUR CLIENT ID }",
  "ClientSecret": "{ YOUR SECRET FROM THE SANDBOX }"
}
```

- Open `DemoHost\wwwroot\js\script.js`
- Update with your URL below and save (you'll find URL under the "Account" tab):

```js
// instantiate weavy
var weavy = window.weavy = new Weavy({
    url: "https://{ YOUR WEAVY URL }",
    jwt: getToken
});
```


## Run site with Weavy added

- Navigate to `DemoHost` in the terminal
- Enter `dotnet run`
- Open a browser and navigate to the URL indicated by the logs in the terminal. e.g. https://localhost:7059 (use the HTTPS one!)
- Sign in and see Weavy in action
    - Predefined users (password doesn't matter, it's a dumbed down login for demo purposes):
       - rickard (belongs to Directory A)
       - nina (belongs to Directory A)
       - lydia (belongs to Directory A)
       - johan (belongs to Directory B)
       - magnus (belongs to Directory B)
       - dave (belongs to Directory B)

    - If you login with any other name that user will be created and added to Directory C
    - See `HomeController.cs` for the more info
