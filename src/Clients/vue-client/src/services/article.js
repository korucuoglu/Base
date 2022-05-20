import { http } from '../utils/http'
import router from '@/router'

const service = {
  add(userData) {
    http.post('/articles', userData).then(({ data }) => {
      router.push({ name: 'ArticleDetailsView', params: { id: data.value } })
    })
  },

  update(userData) {
    http.put('/articles', userData).then(() => {
      router.push({ name: 'ArticleDetailsView', params: { id: userData.id } })
    })
  },

  async getPublicNotes() {
    var result = await http.get('articles/public')
    return result.data?.value
  },

  async getById(id) {
    var result = await http.get(`articles/${id}`)
    return result.data?.value
  },
  async getMyNotes() {
    var result = await http.get(`articles`)
    return result.data?.value
  },
  async getArticlesByCategoryId(id) {
    var result = await http.get(`categories/${id}/articles`)
    return result.data?.value
  },
}

export default service
