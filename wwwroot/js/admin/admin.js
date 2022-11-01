const router = new VueRouter({
    routes: [
        { path: "/", component: clientsComponent },
        { path: "/messages", component: messagesComponent },
        { path: "/blog", component: blogComponent }
    ]
});

new Vue({
    el: "#app",
    data: {
        
    },
    router
});