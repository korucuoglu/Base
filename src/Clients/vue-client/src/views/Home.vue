<template>
  <NoteList :items="items" />
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
    this.items = await NoteService.getPublicNotes()
  },
}
</script>
