<script>
    import {Doughnut} from 'vue-chartjs'

    export default {

        extends: Doughnut,

        props:{

            chartData :{
        
                type: Array,
                
                required:false
            
            },
            
            chartLabel:{
            
                type:Array,
                
                required:false
            
            },

            // option:{
            
            //     type:Object,
                
            //     required:false
            
            // }
        
        },
        mounted () {

            this.renderChart({
        
                    labels: this.chartLabel,

                    datasets: [
                    
                        {
                            label: 'Email Statistics',
                        
                            data: this.chartData,
                            
                            backgroundColor: ["#bb8b30","#1a7bb9","#ed5565","#262626","#23c6c8","#f8ac59"]
                        
                        }
                    ]
                    
            }, {responsive: true, maintainAspectRatio: false})

            this.$refs.canvas.onclick = (evt)=>{
                let index = this.$data._chart.getElementsAtEvent(evt)[0]._index
                alert(this.chartLabel[index]+'='+this.chartData[index])

            }

        }
    }

</script>
