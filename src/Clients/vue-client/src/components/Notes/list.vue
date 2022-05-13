<template>
  <div class="container mt-5">
    <h1 class="text-center">Public Notes</h1>
    <br />
    <div class="row">
      <NotesItem v-for="item in items" :key="item.id" :item="item" />
    </div>
  </div>
</template>

<script>
import NotesItem from '@/components/Notes/item'
export default {
  name: 'Home',
  components: {
    NotesItem,
  },

  props: ['items'],
}
</script>
