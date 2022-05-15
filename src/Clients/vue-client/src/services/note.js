import { http } from '../utils/http'

const service = {
  async getPublicNotes() {
    var result = await http.get('notes/public')
    return result.data?.value
  },
  async getById(id) {
    var result = await http.get(`notes/${id}`)
    return result.data?.value
  },
}

export default service
