import axios from 'axios'
import store from '@/store'
import router from '@/router'
import { useToast, TYPE } from 'vue-toastification'

export const http = axios.create({
  baseURL: 'http://localhost:5000',
  header: {
    'X-Application-Name': 'vue',
    'Content-Type': 'application/json',
  },
})

http.interceptors.request.use(function (config) {
  if (store.getters['users/isAuthenticated']) {
    const token = `Bearer ${store.getters['users/currentUserToken']}`
    config.headers.Authorization = token
  }
  return config
})

http.interceptors.response.use(
  function (response) {
    if (response.data.message) {
      useToast()(response.data.message, {
        type: TYPE.SUCCESS,
      })
    }

    if (response.status === 204) {
      useToast()('Başarılı', {
        type: TYPE.SUCCESS,
      })
    }
    return response
  },
  function (error) {
    if (error.response.data.message) {
      useToast()(error.response.data.message, {
        type: TYPE.ERROR,
      })
    }

    if (error.response.status === 401) {
      useToast()('Lütfen giriş yapınız', {
        type: TYPE.ERROR,
      })
      if (store.getters['users/isAuthenticated']) {
        store.commit('users/setUser', null)
      }
      router.push({ name: 'LoginView' })
    }

    return Promise.reject(error)
  }
)

// http.interceptors.response.use(function (response) {
//   if (response.data.message) {
//     useToast(response.data.message, {
//       type: response.data.isSuccessful ? TYPE.SUCCESS : TYPE.WARNING,
//     })
//   }
//   console.log('axios test', response)
//   return response
// })
