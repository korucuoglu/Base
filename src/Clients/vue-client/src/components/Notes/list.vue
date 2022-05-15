<template>
  <div class="row">
    <NotesItem v-for="item in items" :key="item.id" :item="item" />
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
