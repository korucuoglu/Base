import { http } from '../utils/http'

const service = {
  async getAll() {
    var result = await http.get(`categories`)
    return result.data?.value
  },
}

export default service
