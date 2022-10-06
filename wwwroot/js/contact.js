Vue.component("contact-form", {
    props: {
        reference: {
            required: true,
            default: 0,
            type: Number
        }
    },
    data: function() {
        return {
            formObj: {
                name: "",
                lastName: "",
                email: "",
                subject: 0,
                message: ""
            },
            acceptPolicy: false,
            validationStatus: {
                name: {
                    hasInput: false
                },
                lastName: {
                    hasInput: false
                },
                email: {
                    hasInput: false
                },
                subject: {
                    hasInput: false
                },
                message: {
                    hasInput: false
                }
            },
            success: false,
            submitted: false,
            submitting: false
        }
    },
    methods: {
        emailIsValid: function () {
            const emailValidationRegex = new RegExp("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$");
            return emailValidationRegex.test(this.formObj.email);
        },
        submit: async function() {
            this.submitting = true;
            const customHeaders = new Headers();
            customHeaders.append("Content-Type", "application/json")
            try {
                const response = await fetch("/api/contact/submit", {
                    method: "POST",
                    headers: customHeaders,
                    body: JSON.stringify(this.formObj)
                })
                this.submitted = true;
                this.submitting = false;
                this.success = response.ok;
            } catch(e) {
                this.submitted = true;
                this.submitting = false;
                this.success = false;
            }
        }
    },
    watch: {
        reference: {
            immediate: true,
            handler: function(val, old) {
                this.formObj.subject = val;
            }
        },
        formObj: {
            deep: true,
            handler: function(value, oldValue) {
                for(const prop in oldValue) {
                    if(this.formObj[prop].length > 0){
                        this.validationStatus[prop].hasInput = true
                    }
                }
            }
        }
    },
    computed: {
        formValidation: function(){
            return {
                name: {
                    isValid: this.formObj.name.trim().length > 0
                },
                lastName: {
                    isValid: this.formObj.lastName.trim().length > 0
                },
                email: {
                    isValid: this.emailIsValid()
                },
                subject: {
                    isValid: this.formObj.subject >= 0 && this.formObj.subject <= 3
                },
                message: {
                    isValid: this.formObj.message.trim().length >= 100
                }
            }
        },
        isValid: function() {
            return this.formValidation.name.isValid && 
                this.formValidation.lastName.isValid && 
                this.formValidation.email.isValid && 
                this.formValidation.subject.isValid && 
                this.formValidation.message.isValid;
        }
    },
    template: `
    <div class="column">
        <div class="columns">
            <div class="column"></div>
           
            <div class="column">
                <div class="section">
                    <h1 class="title">Contáctenos</h1>
                    <p>
                        Rellene el formulario para realizar un primer contacto con nosotros. Le contactaremos para 
                        informarle sobre el estado de su solicitud.
                    </p>
                </div>
                <div class="section" v-if="!submitted">
                    <div class="field">
                        <label class="field-label">Nombre</label>
                        <div class="control has-icons-right">
                            <input class="input" :class="{'is-danger': !formValidation.name.isValid && validationStatus.name.hasInput}" type="text" v-model="formObj.name">
                            <span class="icon is-small is-right" v-if="!formValidation.name.isValid && validationStatus.name.hasInput">
                              <i class="fas fa-exclamation-triangle"></i>
                            </span>
                        </div>
                        <p class="help is-danger" v-if="!formValidation.name.isValid && validationStatus.name.hasInput">Debe ingresar su nombre(s)</p>
                    </div>
                    <div class="field">
                        <label class="field-label">Apellidos</label>
                        <div class="control has-icons-right">
                            <input class="input" type="text" :class="{'is-danger': !formValidation.lastName.isValid && validationStatus.lastName.hasInput}" v-model="formObj.lastName">
                            <span class="icon is-small is-right" v-if="!formValidation.lastName.isValid && validationStatus.lastName.hasInput">
                              <i class="fas fa-exclamation-triangle"></i>
                            </span>
                        </div>
                        <p class="help is-danger" v-if="!formValidation.lastName.isValid && validationStatus.lastName.hasInput">Debe ingresar al menos un apellido</p>
                    </div>
                    <div class="field">
                        <label class="field-label">Correo electrónico</label>
                        <div class="control has-icons-left has-icons-right">
                            <input class="input " type="email" :class="{'is-danger': !formValidation.email.isValid && validationStatus.email.hasInput}" v-model="formObj.email">
                            <span class="icon is-small is-left">
                                <i class="fas fa-envelope"></i>
                            </span>
                            <span class="icon is-small is-right" v-if="!formValidation.email.isValid && validationStatus.email.hasInput">
                              <i class="fas fa-exclamation-triangle"></i>
                            </span>
                        </div>
                        <p class="help is-danger" v-if="!formValidation.email.isValid && validationStatus.email.hasInput">Dirección de correo no válida</p>
                    </div>
                    <div class="field">
                        <label class="field-label">Asunto</label>
                        <div class="control">
                            <div class="select is-fullwidth">
                                <select :class="{'is-danger': !formValidation.subject.isValid && validationStatus.subject.hasInput}" v-model="formObj.subject">
                                    <option :value="0">Desarrollo de software nuevo</option>
                                    <option :value="1">Mantenimiento o actualización de software legacy</option>
                                    <option :value="2">Tareas de infraestructura</option>
                                    <option :value="3">Isolaatti</option>
                                    <option :value="4">Otro</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="field">
                        <label class="field-label">Cuéntenos</label>
                        <div class="control has-icons-right">
                            <textarea :class="{'is-danger': !formValidation.message.isValid && validationStatus.message.hasInput}" class="textarea" v-model="formObj.message"></textarea>
                            <span class="icon is-small is-right" v-if="!formValidation.message.isValid && validationStatus.message.hasInput">
                              <i class="fas fa-exclamation-triangle"></i>
                            </span>
                        </div>
                        <p class="help">{{formObj.message.trim().length}} / 100</p>
                        <p class="help is-danger" v-if="!formValidation.message.isValid && validationStatus.message.hasInput">El texto debe tener al menos 100 caracteres</p>
                    </div>
                    <label class="checkbox">
                        <input type="checkbox" v-model="acceptPolicy">
                        Acepta la <a href="/aviso_de_privacidad">política de manejo de datos</a>
                    </label>
                    <div class="is-flex is-justify-content-end">
                        <button type="submit" class="button is-primary" :class="{'is-loading': submitting}" :disabled="!this.isValid || !acceptPolicy" @click="submit" >Enviar</button>
                    </div>
                </div>
                <div class="section" v-else>
                    <article class="message is-success" v-if="success">
                        <div class="message-header">
                          <p>Enviado</p>
                        </div>
                        <div class="message-body">
                          Su consulta ha sido envíada, nos pondremos en contacto con usted pronto.
                        </div>
                    </article>
                    <article class="message is-danger" v-else>
                        <div class="message-header">
                          <p>Ha ocurrido un error</p>
                        </div>
                        <div class="message-body">
                            <div class="is-flex is-flex-direction-column">
                                <span>Ocurrió un error al enviar su consulta.</span>
                                <button class="button is-primary" :class="{'is-loading': submitting}" @click="submit">Reintentar</button>
                           </div>
                        </div>
                       
                    </article>
                </div>
            </div>
             <div class="column">
                
            </div>
       </div>
    </div>
    `
})

new Vue({
    el: "#app",
    data: {
        
    },
    methods: {
        
    },
    computed: {
        
    }
});