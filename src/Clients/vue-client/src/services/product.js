import { http } from '../utils/http'

const service = {
  async getAll() {
    var result = await http.get('products')
    return result.data
  },
  async GetById(id) {
    var result = await http.get(`products/${id}`)
    return result.data
  },
}

export default service
