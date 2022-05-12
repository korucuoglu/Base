export default {
  methods: {
    copy(myObject) {
      return JSON.parse(JSON.stringify(myObject))
    },
  },
}
