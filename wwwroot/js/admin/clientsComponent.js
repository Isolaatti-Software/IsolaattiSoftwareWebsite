const clientsComponent = {
    template: `
        <div>
            <p>Clientes</p>
            <ul>
                <li>Mostrar una lista de los clientes por nombre, en orden de entrada</li>
                <li>Cada elemento de esa lista debe poder mandar a una pagina del cliente donde se muestren sus datos</li>
                <li>Debe incluir un boton para crear una nueva cuenta de usuario para cliente.</li>
                <li>Cada pagina de cliente debe mostrar todos los mensajes mandados antes por contactanos. Esto se hace por correspondencia de la dirección de correo.</li>
            </ul>
        </div>
    `
}

Vue.component('clients', clientsComponent);