<script>

    import Helpers from '../../helpers'

    import { EventBus } from '../../event.js';

    import {Bar} from 'vue-chartjs'

    export default {
    
        props:['chartData'],

        extends: Bar,

        data(){

            return {

                helpers :  new Helpers(),

                options : {

                    scales: {

                            xAxes: [{

                                ticks: {

                                    autoSkip: false

                                } 
                            }],

                    },

                    responsive: true,

                    maintainAspectRatio: false,

                }

            }
        },
        mounted () {


            let barData = this.helpers.createKeyMapping({data:this.chartData,keyName:'value'})

            this.renderChart({
            
            labels : barData.labels,
            
            datasets:[
            
                {
                
                    label : "Received",
                    
                    backgroundColor : '#A3E1D4',
                    
                    pointBorderColor : "#fff",
                    
                    data : barData.data
                
                },
                
            ]
            
            }, this.options)

            this.$refs.canvas.onclick = (evt)=>{
                    
                    let index =  this.$data._chart.getElementAtEvent(evt)[0]._index
                   
                    // emitting event

                    EventBus.$emit('showTopReceived',{email:barData.labels[index]})

            }    


        }

        
    }   

</script>
