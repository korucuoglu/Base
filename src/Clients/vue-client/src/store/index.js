import { createStore } from 'vuex'
import users from '@/store/modules/users'
import categories from '@/store/modules/categories'
import createPersistedState from 'vuex-persistedstate'
import SecureLS from 'secure-ls'
const ls = new SecureLS({ isCompression: false })

export default createStore({
  state: {},
  mutations: {},
  actions: {},
  modules: {
    users,
    categories,
  },
  plugins: [
    createPersistedState({
      storage: {
        getItem: (key) => ls.get(key),
        setItem: (key, value) => ls.set(key, value),
        removeItem: (key) => ls.remove(key),
      },
    }),
  ],
})
