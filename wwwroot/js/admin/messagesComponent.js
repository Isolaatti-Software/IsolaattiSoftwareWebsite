const messagesComponent = {
    template: `
        <div>
            <p>Mensajes</p>
            <ul>
                <li>Mostrar los mensajes dejados en el formulario contactanos</li>
                <li>Debe poderse abrir el mensaje en otra pagina y poder ver la información. Esto incluye poder crear una cuenta de usuario utilizando los datos mostrados aqui</li>
            </ul>
        </div>
    `
}

Vue.component('messages', messagesComponent);