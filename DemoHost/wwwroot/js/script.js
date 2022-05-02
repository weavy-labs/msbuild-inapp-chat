// fetch token for logged in user from backend
var getToken = function () {
    return fetch('/token').then(response => response.text());
};

// instantiate weavy
var weavy = window.weavy = new Weavy({
    url: "https://hansson.weavy.io",
    jwt: getToken
});

// create app
var messenger = weavy.app({
    id: "my-messenger",
    type: "messenger",
    container: "#messenger-container",
    open: false,
    badge: true,
})

// toggle messenger
document.getElementById("messenger-button").addEventListener("click", () => messenger.toggle());

// handle markup changes
messenger.on("app-open", () => document.getElementById("messenger-container").classList.add("open"))
messenger.on("app-close", () => document.getElementById("messenger-container").classList.remove("open"));

