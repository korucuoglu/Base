<template>
  <div class="container mt-5">
    <h1 class="text-center">My Notes</h1>
    <br />
    <NoteList :items="items" />
  </div>
</template>

<script>
import NoteList from '@/components/Notes/list'
import NoteService from '@/services/note'
export default {
  name: 'Home',
  components: {
    NoteList,
  },
  data() {
    return {
      items: [],
    }
  },

  async mounted() {
    this.items = await NoteService.getMyNotes()
  },
}
</script>
