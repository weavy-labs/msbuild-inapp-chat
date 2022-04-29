// fetch token for logged in user from backend
var getToken = function () {
    return fetch('/token').then(response => response.text());
};

// instantiate weavy
var weavy = window.weavy = new Weavy({
    url: "https://jojje.weavy.io",
    jwt: getToken,
    stylesheet: [
        "/css/weavy-dark.css",
    ],
});

// create app
var messenger = weavy.app({
    id: "my-messenger",
    type: "messenger",
    container: "#drawer",
    open: false,
    badge: true,
    className: "messenger-x"
})

// toggle messenger
document.getElementById("messenger").addEventListener("click", () => messenger.toggle());

// handle markup changes
messenger.on("app-open", () => document.getElementById("drawer").classList.add("open"))
messenger.on("app-close", () => document.getElementById("drawer").classList.remove("open"));

// responds to the badge event
messenger.on("badge", (e, badge) => { setBadge("#messenger", badge.count) });

// displays the badge
function setBadge(selector, count) {
    var badge = document.querySelector(selector + " .badge");
    if (typeof count === 'number') {
        if (count > 0) {
            if (!badge) {
                badge = document.createElement("span");
                badge.className = "badge bg-danger";
                document.querySelector(selector).append(badge);
            }
            badge.innerText = count;
        } else {
            if (badge) {
                badge.remove();
            }
        }
    }
}

