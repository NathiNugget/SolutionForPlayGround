class Playground{
  constructor(id, name, maxChildren, minAge){
    this.id = id; 
    this.name = name; 
    this.maxchildren = maxChildren; 
    this.minage = minAge; 
  }
}

const app = Vue.createApp({
    data() {
      return {
        playgrounds: [],
        newId: NaN,
        newName: "",
        newMaxChildren: NaN, 
        newMinAge: NaN, 
      }
    },
    methods: {
      async TryGet(){
        const response = axios.get("https://examtestmock.azurewebsites.net/PlayGrounds").then(
          (response) => {
            this.playgrounds = response.data;
            
          }
        );
      }, 
      async POST(){
        const newPgr = new Playground(0, this.newName, this.newMaxChildren, this.newMinAge); 
        const response = axios.post("https://examtestmock.azurewebsites.net/PlayGrounds", newPgr).then(
          (response) => {
            this.playgrounds.push(response.data); 
          }
        );
      },
      
      async PUT(){
        const newPgr = new Playground(this.newId, this.newName, this.newMaxChildren, this.newMinAge); 
        const response = axios.put(`https://examtestmock.azurewebsites.net/PlayGrounds/${this.newId}`, newPgr).then(
          (response) => {
            const idx = this.playgrounds.findIndex(elem => elem.id == this.newId);  
            this.playgrounds[idx] = response.data; 
          }
        );
      }, 
    },

    async mounted(){
      await this.TryGet();
    },

    computed: {
        
    },
  })
  
  app.mount('#app')

