<template>
    <b-list-group-item class="sub-item bg-light">
        <div class="sub-item-handle mr-2" v-if="!editingSubItem">
            <b-icon-list></b-icon-list>
        </div>
        
        <div class="sub-item-checkbox-wrapper" v-if="!editingSubItem">
            <b-form-checkbox v-model="itemCompletedState">
            </b-form-checkbox>
        </div>

        <div class="sub-item-name" @click="editingSubItem = true;" v-if="!editingSubItem">
            {{ name }}
        </div>

        <b-form @submit.prevent="updateSubItem" v-if="editingSubItem" class="edit-sub-item-form">
            <b-form-group>
                <b-form-input v-model="name" class="mr-2"></b-form-input>
            </b-form-group>

            <b-button
                size="sm"
                class="mr-1"
                variant="success"
                type="submit">
                Save
            </b-button>
            <b-button
                size="sm"
                variant="secondary"
                @click="editingSubItem = false;">
                Cancel
            </b-button>
        </b-form>

    </b-list-group-item>
</template>

<script>
export default {
    props: ['name'],
    data() {
        return {
            editingSubItem: false,
            itemCompletedState: false
        }
    },
    methods: {
        updateSubItem() {

        }
    },
}
</script>

<style lang="scss">

    .sub-item {

        transition: background-color .3s ease;
        
        &.list-group-item {
            display: flex;
            align-items: center;
            padding: 0;
        }

        &:hover {
            cursor: pointer;
            background-color: darken(#f8f9fa, 3) !important;
        }

        &:active {
            background-color: darken(#f8f9fa, 5) !important;
        }

        .sub-item-handle {

            &:hover {
                cursor: move;
            }

            padding: 12px 0px 12px 20px;
        }

        .sub-item-name {
            font-family: 'Nunito', sans-serif;
            font-weight: bold;
            flex: 1 0 auto;
            padding: 12px 0;
        }
    }

    .edit-sub-item-form {
        padding: 12px;
        width: 100%;
    }

</style>