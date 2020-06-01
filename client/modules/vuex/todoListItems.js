import Vue from 'vue';
import axios from 'axios';

const todoLists = {
    state: () => ({
        items: {}
    }),
    mutations: {
        updateItems(state, payload) {
            state.items[payload[0].listId] = payload;
        },
        addItem(state, { listId, item }) {
            state.items[listId].unshift(item);
        },
        updateItemCompletedState(state, { item }) {
            let index = state.items[item.listId].findIndex(i => i.id === item.id);
            state.items[item.listId][index].completed = item.completed;
        },
        updateItem(state, { item }) {
            let index = state.items[item.listId].findIndex(i => i.id === item.id);
            Vue.set(state.items[item.listId], index, item);
        }
    },
    actions: {
        loadItemsByListId(context, payload) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'GET',
                    url: `api/lists/${payload.todoListId}/todos`
                })
                .then((response) => {
                    context.commit('updateItems', response.data);
                })
                .finally(() => {
                    resolve();
                });
            });
        },
        addItem(context, payload) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'POST',
                    url: `api/lists/${payload.listId}/todos`,
                    data: JSON.stringify(payload),
                    headers: {
                        'content-type': 'application/json'
                    }
                })
                .then((response) => {

                })
                .finally(() => {
                    resolve();
                });
            });
        },
        toggleItemCompletedState(context, { listId, itemId, completed }) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'PUT',
                    url: `api/lists/${listId}/todos/${itemId}/completed`,
                    data: JSON.stringify({ completed }),
                    headers: {
                        'content-type': 'application/json'
                    }
                })
                .finally(() => {
                    resolve();
                });
            });
        },
        updateItem(context, { item }) {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'PUT',
                    url: `api/lists/${item.listId}/todos/${item.id}`,
                    data: JSON.stringify({
                        name: item.name,
                        notes: item.notes,
                        dueDate: item.dueDate
                    }),
                    headers: {
                        'content-type': 'application/json'
                    }
                })
                .finally(() => {
                    resolve();
                });
            });
        }
    },
    getters: {
        getItemsByListId: (state) => (listId) => {
            return state.items[listId];
        },
        getItemName: (state) => (listId, itemId) => {
            return state.items[listId].find(i => i.id === itemId).name;
        },
        getItemCompletedState: (state) => (listId, itemId) => {
            return state.items[listId].find(i => i.id === itemId).completed;
        }
    }
}

export default todoLists;