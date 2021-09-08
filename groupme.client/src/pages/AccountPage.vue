<template>
  <div class="about text-center container">
    <div class="row">
      <div class="col">
        <h1>Groups: </h1>
      </div>
    </div>
    <div class="row mt-5">
      <div class="col" v-if="!loaded">
        <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
      </div>
      <div class="col-4" v-else v-for="g in groups" :key="g.id">
        <Group :group="g" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from 'vue'
import { AppState } from '../AppState'
import { groupsService } from '../services/GroupsService'
import Pop from '../utils/Notifier'

export default {
  name: 'Account',
  setup() {
    const loaded = ref(false)
    onMounted(async() => {
      try {
        await groupsService.getMyGroups()
        loaded.value = true
      } catch (error) {
        Pop.toast('Something has gone wrong', 'error')
      }
    })
    return {
      loaded,
      account: computed(() => AppState.account),
      groups: computed(() => AppState.myGroups)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
