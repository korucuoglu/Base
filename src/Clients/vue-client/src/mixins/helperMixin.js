import moment from 'moment'

export default {
  methods: {
    copy(myObject) {
      return JSON.parse(JSON.stringify(myObject))
    },
    timesAgo(time) {
      return moment(time).format('D MMM YYYY')
    },
  },
}
