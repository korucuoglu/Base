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
    _myArticles: (state) => (categoryId) => {
      if (categoryId) {
        return state.list.filter((item) => item.categoryId === categoryId)
      }
      return state.list
    },
  },
}
