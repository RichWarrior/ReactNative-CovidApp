import axios from 'axios'

const BASE_URL = 'https://6b1944c7afbb.ngrok.io'

const ApiClient = {    
    Get (resource,params){
        return new Promise((resolve,reject)=>{
            axios.get(`${BASE_URL}${resource}`,params).then((response)=>{                
                resolve(response.data)
            }).catch((err)=>{
                reject(err)
            })
        })
    },

    Post(resource,params){
        return new Promise((resolve,reject)=>{
            axios.post(`${BASE_URL}${resource}`,params).then((response)=>{                
                resolve(response.data)
            }).catch((err)=>{
                console.log(err)
                reject(err)
            })
        })
    }
}


export default ApiClient;
