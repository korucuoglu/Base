import { http } from '../utils/http'
import store from '@/store'

const service = {
  login(data) {
    http.post('users/login', data).then(({ data }) => {
      const loginData = { token: data.value, info: this.parseJwt(data.value) }
      store.commit('users/setUser', loginData)
    })
  },
  async update(data) {
    var result = await http.put('users', data)
    return result
  },

  parseJwt(token) {
    var base64Url = token.split('.')[1]
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
    var jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
        })
        .join('')
    )
    return JSON.parse(jsonPayload)
  },
}

export default service
