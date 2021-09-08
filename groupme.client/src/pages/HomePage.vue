<template>
  <div class="home flex-grow-1 container">
    <div class="row mt-5">
      <div class="col" v-for="g in groups" :key="g.id">
        <Group :group="g" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { logger } from '../utils/Logger'
import { groupsService } from '../services/GroupsService'
import { AppState } from '../AppState'
export default {
  setup() {
    onMounted(async() => {
      try {
        await groupsService.getAllGroups()
      } catch (error) {
        Pop.toast('Something Has gone Totally wrong', 'error')
        logger.log(error)
      }
    })
    return {
      groups: computed(() => AppState.groups)
    }
  }
}
</script>

<style scoped lang="scss">

</style>
