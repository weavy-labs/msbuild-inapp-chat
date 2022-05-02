
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
    id: userDirectory,
    type: "messenger",
    container: "#messenger-container",
    open: false,
    badge: true,
    stylesheet: "/css/dark-theme.css"
})

// toggle messenger
document.getElementById("messenger-button").addEventListener("click", () => messenger.toggle());

// handle markup changes
messenger.on("app-open", () => document.getElementById("messenger-container").classList.add("open"))
messenger.on("app-close", () => document.getElementById("messenger-container").classList.remove("open"));

// responds to the badge event
messenger.on("badge", (e, badge) => { setBadge("#messenger-button", badge.count) });

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

// swith theme
document.getElementById("blue-theme").addEventListener("click", () => messenger.addStyles({ css: ".appbar-light { background: #176c94 } .appbar-light .btn-link:not(.btn-color), .appbar-light .btn-link:not(.btn-color):hover  { color:#fff } .appbar-right .btn-icon.btn-primary { color:#fff } " }));
document.getElementById("sand-theme").addEventListener("click", () => messenger.addStyles({ css: ".appbar-light { background: #ffeeba } .appbar-light .btn-link:not(.btn-color), .appbar-light .btn-link:not(.btn-color):hover  { color:#176c94 } .appbar-right .btn-icon.btn-primary { color:#176c94 } " }));
document.getElementById("default-theme").addEventListener("click", () => messenger.addStyles({ css: ".appbar-light { background-color: rgba(242,242,242,.75); } .appbar-light .btn-link:not(.btn-color), .appbar-light .btn-link:not(.btn-color):hover  { color: rgba(0,0,0,.69); } .appbar-right .btn-icon.btn-primary { color:#156b93; } " }));
