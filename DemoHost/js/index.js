import Weavy from '@weavy/dropin-js';

//Weavy.defaults.logging = { debug: true };

var getToken = function () {
    return fetch('/token').then(response => response.text());
};

var weavy = window.weavy = new Weavy({
    url: "https://jojje.weavy.io",
    jwt: getToken,
    stylesheet: [
        "/css/weavy-dark.css",
    ],
});

var messenger = weavy.app({
    id: "my-messenger",
    type: "messenger",
    container: "#drawer",
    open: false,
    badge: true,
    //css: ".conversation .list-group-item { background: red; }",
    stylesheet: [
        //"/css/weavy-messenger.css",
        //"/css/weavy-dark.css",
    ],
    className: "messenger-x"
})

//messenger.addStyles("#my-messenger .conversation .list-group-item { color: white; }");

document.getElementById("messenger").addEventListener("click", () => messenger.toggle());

messenger.on("app-open", () => document.getElementById("drawer").classList.add("open"))
messenger.on("app-close", () => document.getElementById("drawer").classList.remove("open"));
messenger.on("badge", (e, badge) => { setBadge("#messenger", badge.count) });

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

