const messagesComponent = {
    data: function() {
        return {
            lastId: null,
            messages: [],
            moreContent: true
        }
    },
    computed: {
        url: function() {
            let urlBase = "/api/admin/messages/get?count=20";
            if(this.lastId !== null) {
                urlBase += `&?lastId=${this.lastId}`;
            }
            
            return urlBase;
        }
    },
    methods: {
        fetchMessages: async function() {
            const response = await fetch(this.url);
            const dto = await response.json();
            this.messages = this.messages.concat(dto);

            this.moreContent = dto.length === 0
        },
        emailProfileUrl: function(email) {
            return `/Admin/EmailProfile?email=${email}`;
        }
    },
    mounted: async function() {
        await this.fetchMessages();
    },
    template: `
        <div>
            <div class="is-flex is-flex-direction-column">
                <div class="card" v-for="message in messages">
                    <div class="card-content">
                        <p><strong>{{message.name}} {{message.lastName}}</strong></p>
                        <p><a :href="'mailto:' + message.email">{{message.email}}</a></p>
                        <div class="is-flex">
                            <a :href="emailProfileUrl(message.email)" class="button">Ver perfil</a>
                        </div>
                        <p>{{message.message}}</p>
                    </div>
                </div>
            </div>
        </div>
    `
}

Vue.component('messages', messagesComponent);