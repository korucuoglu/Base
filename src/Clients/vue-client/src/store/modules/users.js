import { http } from '@/utils/http'
import router from '@/router'

export default {
  namespaced: true,
  state: {
    user: null,
  },
  mutations: {
    setUser(state, data) {
      state.user = data
      router.push({ name: 'HomeView' })
    },
    logout(state) {
      state.user = null
      router.push({ name: 'LoginView' })
    },
  },
  actions: {
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
    isAuthenticated: (state) => state.user !== null,
    currentUserToken: (state) => state.user?.token,
    currentUser: (state) => state.user?.info,
  },
}
