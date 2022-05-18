import moment from 'moment'

export default {
  methods: {
    copy(myObject) {
      return JSON.parse(JSON.stringify(myObject))
    },
    timesFormat(time) {
      return moment(time).format('D MMM YYYY')
    },
  },
}
