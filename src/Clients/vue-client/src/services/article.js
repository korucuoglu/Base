import { http } from '../utils/http'

const service = {
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
