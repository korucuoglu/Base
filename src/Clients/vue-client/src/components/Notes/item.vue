<template>
  <div class="col-10 offset-1">
    <div class="card mb-2">
      <div class="card-body">
        <router-link
          :to="{ name: 'NoteDetailsView', params: { id: item.id } }"
          class="card-title h5"
          >{{ item.title }}</router-link
        >
        <h6 class="card-subtitle mb-2 text-muted">{{ item.username }}</h6>
        <p class="card-text">
          {{ item.content }}
        </p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['item'],
}
</script>

<style>
.card-text {
  width: 100%;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
}
</style>
