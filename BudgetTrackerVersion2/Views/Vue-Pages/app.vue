<template>
    <div>
        <div id="login">
            <div id="MyModal" class="modal" data-backdrop="static">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Create User</h5>                            
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="username">Username</label>
                                <input type="text" v-model="user.username" class="form-control" id="exampleInputPassword1" placeholder="Username">
                            </div>
                            <div class="form-group">
                                <label for="Password">Password</label>
                                <input type="password" v-model="user.password" class="form-control" id="Password" placeholder="Password">
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" v-on:click="submitUser" class="btn btn-primary" data-dismiss="modal">Submit</button>                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="main">
            <div class="row">
                <app-Income v-on:incomeSubmitted="getLog('GetIncomeItems')" v-on:addIncomeToBalance="increaseBalance"></app-Income>
                <app-Expense v-on:expenseSubmitted="getLog('GetExpenseItems')"></app-Expense>
            </div>
            <button v-on:click="deleteItems">Delete Items</button>

            <div class="row">
                <div class="col-lg-12">
                    <div v-if="showActivity" id="activityContainer" class="card text-white bg-secondary">
                        <div class="row">
                            <div class="col-lg-5">
                                <div class="form-row ml-2 mt-1 mb-1">
                                    <label class="col-lg-8" style="font-size: 19px">{{currentLogSetting}} Log</label>
                                    <select v-on:change="onChange($event)" class="form-control-sm col-lg-4 w-35">
                                        <option>Income</option>
                                        <option>Expense</option>
                                        <option>Income & Expense</option>
                                    </select>
                                </div>
                                <table class="table table-hover table-bordered table-sm" style="margin-left: 5px">
                                    <thead>
                                        <tr class="table-light text-secondary">
                                            <th scope="col">Description</th>
                                            <th scope="col">Cost</th>
                                            <th scope="col">Date</th>
                                        </tr>
                                    </thead>
                                    <tbody v-if="logPathProperty == 'GetIncomeItems'">
                                        <tr v-for="(item, index) in recentIncomeItems" v-bind:class="index % 2 == 0 ? 'table-default' : 'table-light'" v-bind:key="index">
                                            <td class="IncomeLogItem">{{item.IncomeDescription}}</td>
                                            <td>{{item.IncomeAmount}}</td>
                                            <td>{{onlyShowDate(item.IncomeDate)}}</td>
                                        </tr>
                                    </tbody>
                                    <tbody v-else-if="logPathProperty == 'GetExpenseItems'">
                                        <tr v-for="(item, index) in recentExpenseItems" v-bind:class="index % 2 == 0 ? 'table-default' : 'table-light'" v-bind:key="index">
                                            <td class="ExpenseLogItem">{{item.ExpenseDescription}}</td>
                                            <td>{{item.ExpenseAmount}}</td>
                                            <td>{{onlyShowDate(item.ExpenseDate)}}</td>
                                        </tr>
                                    </tbody>
                                    <tbody v-else>
                                        <tr v-for="(item, index) in recentBudgetItems" v-bind:class="index % 2 == 0 ? 'table-default' : 'table-light'" v-bind:key="index">
                                            <td v-bind:class="item.BudgetItemGroup == 0 ? 'IncomeLogItem' : 'ExpenseLogItem'">{{item.BudgetItemDescription}}</td>
                                            <td>{{item.BudgetItemAmount}}</td>
                                            <td>{{onlyShowDate(item.BudgetItemDate)}}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="col-lg-5 offset-2">
                                <app-Line-Chart></app-Line-Chart>
                            </div>
                        </div>
                    </div>
                    <div v-else id="activityContainerBegin" class="card text-white bg-secondary">
                        <div class="row justify-content-center">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <div v-if="showBalanceError" class="mt-5">A Balance of higher than 0$ must be entered.</div>
                                    <div class="input-group mb-3" v-bind:class="{ 'mt-5': !showBalanceError }">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" style="background-color:#DF691A">$</span>
                                        </div>
                                        <input type="number" v-model="balance" class="form-control text-center" placeholder="$5,000" />
                                    </div>
                                    <button type="button" v-on:click="submitBalance" class="btn btn-primary btn-lg btn-block mb-5 standardButton">Submit Starting Balance</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>    
</template>

<script>    
    import appIncome from './Components/Income.vue';
    import appExpense from './Components/Expense.vue';
    import axios from 'axios';

    var lineChart = {             
        render(h) {
            return h('canvas', {
                attrs: {
                    id: 'ctx',
                    width: '400px',
                    height: '280px'
                }                
            });
        },
        methods: {
            draw: function (ctx) {
                var myLineChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: ['April 19', 'May 19', 'June 19', 'July 19', 'Aug 19'],
                        datasets: [{
                            backgroundColor: 'red',
                            borderColor: 'white',
                            label: 'Net Worth',
                            data: [
                                3.45,
                                5,
                                7,
                                8.1,
                                5
                            ],
                            fill: false
                        }]
                    },
                    options: {
                        responsive: true,
                        legend: {
                            labels: {
                                fontColor: "white"
                            }
                        },
                        title: {
                            display: true,
                            text: 'Total',
                            fontColor: "white"
                        },
                        scales: {
                            xAxes: [{
                                display: true,
                                scaleLabel: {
                                    display: true,
                                    labelString: 'Month',
                                    fontColor: "white"
                                },
                                ticks: {
                                    fontColor: "white"
                                }
                            }],
                            yAxes: [{
                                display: true,
                                scaleLabel: {
                                    display: true,
                                    labelString: 'Amount (in $1,000s)',
                                    fontColor: "white"
                                },
                                ticks: {
                                    fontColor: "white"
                                }
                            }]
                        }
                    }
                });
            }
        },
        mounted: function () {
            var c = document.getElementById("ctx");            
            var ctx = c.getContext("2d");
            this.draw(ctx);
        }
    } 

    export default {        
        components: {
            appIncome,
            appExpense,
            appLineChart : lineChart,
        },
        data: function () {
            return {                
                expenseItems: [],
                incomeItems: [],
                budgetItems: [],
                currentLogSetting: "Income",
                logPath: "GetIncomeItems",
                balance: 0,
                balanceSubmitted: false,
                showBalanceError: false,

                user: {username: "", password: ""}
            }   
        },
        computed: {
            logPathProperty: {
                get: function () {
                    return this.logPath;
                },
                set: function (newlogPath) {
                    this.logPath = newlogPath;
                }
            },
            showActivity: function() {
                if ((this.incomeItems.length || this.expenseItems.length) || this.balanceSubmitted) {
                    return true;
                }
                return false;
            },
            recentIncomeItems: function () {
                if (this.incomeItems) {
                    var sortedIncomeItems = this.incomeItems.sort(function (a, b) {
                        return new Date(b.IncomeDate) - new Date(a.IncomeDate);
                    });

                    var recentIncomeItems = sortedIncomeItems.filter(function (currVal, index) {
                        if (index > 7) { return false; }
                        return true;
                    });

                    return recentIncomeItems;
                } else {
                    return incomeItems
                }                
            },
            recentExpenseItems: function () {
                if (this.expenseItems) {
                    var sortedExpenseItems = this.expenseItems.sort(function (a, b) {
                        return new Date(b.ExpenseDate) - new Date(a.ExpenseDate);
                    });

                    var recentExpenseItems = sortedExpenseItems.filter(function (currVal, index) {
                        if (index > 7) { return false; }
                        return true;
                    });

                    return recentExpenseItems;
                } else {
                    return expenseItems;
                }
            },
            recentBudgetItems: function () {
                if (this.budgetItems) {
                    var sortedBudgetItems = this.budgetItems.sort(function (a, b) {
                        return new Date(b.BudgetItemDate) - new Date(a.BudgetItemDate);
                    });

                    var recentBudgetItems = sortedBudgetItems.filter(function (currVal, index) {
                        if (index > 7) { return false; }
                        return true;
                    });

                    return recentBudgetItems;
                } else {
                    return budgetItems;
                }
            }
        },
        methods: {            
            deleteItems: function () {                
                axios.delete("/Main/DeleteBudgetItems")
                .then(response => {
                    this.expenseItems = [];
                    this.incomeItems = [];
                    this.balanceSubmitted = false;
                    this.showBalanceError = false;
                    this.balance = 0;
                })
                .catch(error => {
                    console.log(error);
                });
            },
            getLog: function (logPath) {
                axios.request({
                    url: `/Main/${logPath}`,
                    method: "get",
                    contentType: "application/json"
                }).then(response => {                    
                    if (logPath == "GetIncomeItems") {
                        this.incomeItems = response.data;
                    } else if (logPath == "GetExpenseItems") {
                        this.expenseItems = response.data;
                    } else if (logPath == "GetBudgetItems") {
                        this.budgetItems = response.data;
                    }
                }).catch(error => {
                    console.log(error);
                })
            },
            increaseBalance: function (e) {
                this.balance += e;
            },
            onChange: function (e) {                
                if (e.target.value) {
                    var value = e.target.value;                    
                    this.currentLogSetting = value;    

                    if (value == "Income") {
                        this.logPathProperty = "GetIncomeItems";
                        this.getLog(this.logPathProperty);
                    } else if (value == "Expense") {
                        this.logPathProperty = "GetExpenseItems";
                        this.getLog(this.logPathProperty);
                    } else if (value == "Income & Expense") {
                        this.logPathProperty = "GetBudgetItems";
                        this.getLog(this.logPathProperty);
                    }
                }                
            },
            onlyShowDate: function (date) {
                var newDate = new Date(date);

                return newDate.toDateString();
            },
            submitBalance: function () {
                if (this.balance > 0) {
                    this.balanceSubmitted = true;
                } else {
                    this.showBalanceError = true;
                }
            },
            submitUser: function () {
                axios.request({
                    url: '/Main/SubmitUser',
                    method: 'post',
                    contentType: 'application/json'
                }).then(response => {
                    console.log("User Submitted");
                }).catch(error => {
                    console.log("Submit User Error Details = " + error);
                })
            }
        },
        mounted: function(){
            this.getLog(this.logPath);

            if (!this.incomeItems.length && !this.expenseItems.length) {
                $('#MyModal').modal();
                debugger;
            }            
        }
    }   

       
</script>