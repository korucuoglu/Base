<template>
  <div class="container mt-5">
    <h1 class="text-center">Add New Article</h1>
    <br />
    <div class="mb-3 d-flex align-items-center justify-content-between">
      <label class="form-label fw-bolder">Title</label>
      <input v-model="userData.title" type="text" class="form-control ms-5" />
    </div>

    <div class="mb-3 d-flex align-items-center justify-content-between">
      <label class="form-label fw-bolder">Content:</label>
      <textarea
        v-model="userData.content"
        rows="10"
        cols="10"
        type="text"
        class="form-control ms-5"
      />
    </div>

    <div class="mb-3 d-flex align-items-center justify-content-between">
      <label class="form-label fw-bolder">Category:</label>
      <select class="form-select ms-5" v-model="userData.categoryId">
        <option disabled>Please select one category</option>
        <option
          v-for="category in categoryList"
          :key="category.id"
          :value="category.id"
        >
          {{ category.title }}
        </option>
      </select>
    </div>

    <div class="mb-3">
      <input
        class="form-check-input"
        type="checkbox"
        v-model="userData.isPublic"
        id="flexCheckDefault"
      />
      <label class="form-check-label" for="flexCheckDefault"> Is Public </label>
    </div>

    <button class="btn btn-primary" @click="onSave">Submit</button>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import articleService from '@/services/article'
export default {
  computed: {
    ...mapGetters({
      categoryList: 'categories/categoryList',
    }),
  },

  data() {
    return {
      userData: {
        title: null,
        content: null,
        categoryId: null,
        isPublic: true,
      },
    }
  },

  methods: {
    onSave() {
      articleService.add(this.userData)
    },
  },
}
</script>
