import { http } from '../utils/http'

const service = {
  async login(data) {
    var result = await http.post('users/login', data)
    return result.data
  },
  async update(data) {
    var result = await http.put('users', data)
    return result
  },
}

export default service
