<template>
  <div v-if="item != null" class="container mt-5">
    <h1 class="text-center text-capitalize">{{ item.title }}</h1>
    <br />
    <div class="row">
      <div class="col-10 offset-1">
        <p class="lead lh-lg">{{ item.content }}</p>
        <p class="h5 text-end">
          written by {{ item.username }} at
          {{ this.timesFormat(item.createdDate) }}
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import ArticleService from '@/services/article'
export default {
  data() {
    return {
      item: null,
    }
  },
  async mounted() {
    this.item = await ArticleService.getById(this.$route.params.id)
  },
}
</script>
