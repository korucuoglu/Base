import { http } from '@/utils/http'

export default {
  namespaced: true,
  state: {
    list: [],
  },
  mutations: {
    setList(state, data) {
      state.list = data
    },
  },
  actions: {
    fetchList({ commit }) {
      http.get('/articles').then(({ data: articleList }) => {
        commit('setList', articleList.value || [])
      })
    },
  },
  getters: {
    _getMyArticles:
      (state, getters, rootState, rootGetters) => (categoryId) => {
        const userName = rootGetters['users/currentUser']?.username
        if (categoryId) {
          return state.list.filter(
            (item) =>
              item.categoryId === categoryId && item.username == userName
          )
        }
        return state.list.filter((item) => item.username == userName)
      },
    _getListById: (state) => (id) => {
      return state.list.find((item) => item.id === id)
    },
    getPublicArticles: (state) => {
      return state.list.filter((item) => item.isPublic === true)
    },
  },
}
