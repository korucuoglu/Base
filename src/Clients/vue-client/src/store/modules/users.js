import { http } from '@/utils/http'
import router from '@/router'

export default {
  namespaced: true,
  state: {
    token: null,
  },
  mutations: {
    setUser(state, token) {
      state.token = token
    },
    logout(state) {
      state.token = null
      router.push({ name: 'LoginView' })
    },
  },
  actions: {
    login({ commit }, userData) {
      http.post('users/login', userData).then(({ data }) => {
        commit('setUser', data.value)
        router.push({ name: 'HomeView' })
      })
    },
    register(_, userData) {
      http.post('/users/register', userData).then(() => {
        router.push({ name: 'LoginView' })
      })
    },
    update({ commit }, userData) {
      http.put('users', userData).then(() => {
        commit('logout')
      })
    },
    changePassword(_, userData) {
      http.put('users/password', userData).then(() => {
        router.push({ name: 'HomeView' })
      })
    },
  },
  getters: {
    currentUserToken: (state) => state.token,
    isAuthenticated: (state) => state.token !== null,
    currentUser(state) {
      if (state.token !== null) {
        const token = state.token
        const base64Url = token.split('.')[1]
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
        const jsonPayload = decodeURIComponent(
          atob(base64)
            .split('')
            .map(function (c) {
              return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
            })
            .join('')
        )
        return JSON.parse(jsonPayload)
      }
    },
  },
}
